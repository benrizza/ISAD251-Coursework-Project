using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PubApplication.Models;
using PubApplication.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using PubApplication.Models.Enum;
using PubApplication.ViewModels.StoredProcedureViewModels;

namespace PubApplication.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ISAD251DBContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ItemsController(ISAD251DBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string ItemName, ItemTypes? ItemType, bool? ItemOnSale, int? PageNumber) //as this is the default view, only show items on sale
        {
            PubItemsViewModel model;
            if (ItemType == null)
            {
                model = _context.GetPubItems(ItemName, ItemOnSale ?? true, PageNumber ?? 0); //true - get only items on sale
            }
            else
            {
                model = _context.GetPubItems(ItemName, ItemOnSale ?? true, PageNumber ?? 0, (ItemTypes)ItemType); //true - get only items on sale
            }

            if (model != null)
            {
                ViewBag.PubItemsViewModel = model;
            }
            object data;
            TempData.TryGetValue("ToastMessage", out data);
            if (data != null)
            {
                ViewBag.Toast = JsonSerializer.Deserialize<ToastAlertViewModel>(data as string);
            }
            return View(new FilterItemsViewModel() { ItemName = ItemName, ItemOnSale = ItemOnSale ?? true, ItemType = ItemType ?? null, PageNumber = PageNumber ?? 0 });
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("User") == null))
            {
                var user = JsonSerializer.Deserialize<PubUsers>(HttpContext.Session.GetString("User")); //if user is an admin then allow them access to the create page
                if (user.UserAccessRank == Models.Enum.UserAccessRank.Admin)
                {
                    return View(new CreateItemViewModel());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                int itemID = (int)id;
                PubItems item = _context.GetPubItem(itemID);

                if (item != null)
                {
                    EditItemViewModel model = new EditItemViewModel
                    {
                        ItemID = itemID,
                        ItemExistingImagePath = item.ItemImagePath,
                        ItemDescription = item.ItemDescription,
                        ItemName = item.ItemName,
                        ItemOnSale = item.ItemOnSale,
                        ItemPrice = item.ItemPrice,
                        ItemStock = item.ItemStock,
                        ItemType = item.ItemType,
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id != null && id > 0) {
                PubItems Item = _context.GetPubItem((int)id);
                if (Item != null)
                {
                    ViewBag.Item = Item;
                    return View(new AddOrderItemViewModel());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddOrderItem(AddOrderItemViewModel model) //add order using view model request and redirect to index page of items with a toast message
        {
            if (ModelState.IsValid)
            {
                PubItems Item = _context.GetPubItem(model.ItemId);
                if (Item != null)
                { //Item exists in database. 
                    if (Item.ItemOnSale) {
                        if (model.ItemQuantity <= Item.ItemStock)
                        {
                            if (model.ItemQuantity > 0 && model.ItemQuantity <= GlobalConstants.MaxItemsPerOrder) //compare against max items per order only rather than item stock as well as the item stock may change when the user actually makes their order.
                            {
                                string Session = HttpContext.Session.GetString("PubSession"); //store session id
                                int OrderBasketID = 0;
                                if (Session != null) //if a session has been created
                                {
                                    PubSessions pubSession = _context.GetPubSession(Session); //get session info
                                    if (pubSession != null) //session exists
                                    {
                                        if (pubSession.OrderBasketId != 0)
                                        {
                                            //add item 
                                            OrderBasketID = pubSession.OrderBasketId;
                                            PubOrderBasketItems item = _context.GetPubOrderBasketItem(pubSession.OrderBasketId, model.ItemId);
                                            if (item != null)
                                            {
                                                //edit item
                                                int newQuantity = model.ItemQuantity + item.ItemQuantity;
                                                if (newQuantity <= Item.ItemStock)
                                                {
                                                    if (newQuantity <= GlobalConstants.MaxItemsPerOrder)
                                                    {
                                                        //edit
                                                        if (_context.EditPubOrderBasketItem(OrderBasketID, model.ItemId, newQuantity))
                                                        {
                                                            TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.Toast("Item Added", String.Format("{0}x {1} was added to your basket. Total Quantity: {2}", model.ItemQuantity, Item.ItemName, newQuantity), Item.ItemImagePath));
                                                            return RedirectToAction("Index");
                                                        }
                                                        else
                                                        {
                                                            TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.DefaultError());
                                                            return RedirectToAction("Index");
                                                            //error item could not be updated
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //max items error
                                                        TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.ItemQuantityMaxError(Item.ItemImagePath));
                                                        return RedirectToAction("Index");
                                                    }
                                                }
                                                else
                                                {
                                                    TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.ItemQuantityStockError(Item.ItemImagePath));
                                                    return RedirectToAction("Index");
                                                    //not enoguh in stock error
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //create new orderbasket and add item
                                            OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                            if (pubSession.UserId != 0)
                                            {
                                                if (_context.GetPubUser(pubSession.UserId) != null) {
                                                    _context.UpdatePubUserOrderBasket(pubSession.UserId, OrderBasketID);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //create a new session and orderbasket and add item
                                        Session = _context.AddPubSession(null, null);
                                        if (Session != null)
                                        {
                                            HttpContext.Session.SetString("PubSession", Session);
                                            OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                        }
                                    }
                                }
                                else //create a new session and orderbasket and add item
                                {
                                    Session = _context.AddPubSession(null, null);
                                    if (Session != null)
                                    {
                                        HttpContext.Session.SetString("PubSession", Session);
                                        OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                    }
                                }

                                if (OrderBasketID != 0)
                                {
                                    if (_context.AddPubOrderBasketItem(OrderBasketID, model.ItemId, model.ItemQuantity))
                                    {
                                        TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.Toast("Item Added", String.Format("{0}x {1} was added to your basket", model.ItemQuantity, Item.ItemName), Item.ItemImagePath));
                                        return RedirectToAction("Index");
                                    }
                                }
                            }
                            else
                            {
                                TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.ItemQuantityMaxError(Item.ItemImagePath));
                                return RedirectToAction("Index");
                            }


                        }
                        else
                        {
                            TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.ItemQuantityStockError(Item.ItemImagePath));
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        //error item not on sale
                    }
                }
            }
            TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.DefaultError());
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        public IActionResult EditOrderItem(int itemID, int itemQuantity) 
        {
            if (itemID > 0 && itemQuantity > 0)
            {
                string Session = HttpContext.Session.GetString("PubSession"); //store session id
                if (Session != null) //if a session has been created
                {
                    PubSessions pubSession = _context.GetPubSession(Session); //get session info
                    if (pubSession != null && pubSession.OrderBasketId != 0) //session and basket exists
                    {
                        int OrderBasketID = pubSession.OrderBasketId;
                        PubItems PubItem = _context.GetPubItem(itemID);
                        PubOrderBasketItems item = _context.GetPubOrderBasketItem(pubSession.OrderBasketId, itemID);
                        if (item != null && PubItem != null)
                        {
                            //edit item
                            if (itemQuantity <= PubItem.ItemStock)
                            {
                                if (itemQuantity <= GlobalConstants.MaxItemsPerOrder)
                                {
                                    //edit
                                    if (_context.EditPubOrderBasketItem(OrderBasketID, itemID, itemQuantity))
                                    {
                                        return PartialView("ToastMessage", ToastAlert.Toast("Item Updated", string.Format("Changed {0} quantity to: {1}.", PubItem.ItemName, itemQuantity), PubItem.ItemImagePath));
                                    }
                                    else
                                    {
                                        return PartialView("ToastMessage", ToastAlert.DefaultError());
                                        //error item could not be updated
                                    }
                                }
                                else
                                {
                                    //max items error
                                    return PartialView("ToastMessage", ToastAlert.ItemQuantityMaxError(PubItem.ItemImagePath));
                                }
                            }
                            else
                            {
                                return PartialView("ToastMessage", ToastAlert.ItemQuantityStockError(PubItem.ItemImagePath));
                                //not enoguh in stock error
                            }
                        }
                    }
                }
            }
            return null;
        }

        [HttpPost]
        public IActionResult AddOrderItemPartial(int id, int? itemQuantity) //add order using an ajax request and return a partial view with toast message
        {
            if (id > 0)
            {
                int quantity = itemQuantity ?? 1;
                if (quantity != 0) { 
                    PubItems Item = _context.GetPubItem(id);
                    if (Item != null)
                    {
                        if (Item.ItemOnSale)
                        {
                            if (quantity <= Item.ItemStock)
                            {
                                if (quantity > 0 && quantity <= GlobalConstants.MaxItemsPerOrder) //compare against max items per order only rather than item stock as well as the item stock may change when the user actually makes their order.
                                {
                                    string Session = HttpContext.Session.GetString("PubSession"); //store session id
                                    int OrderBasketID = 0;
                                    if (Session != null) //if a session has been created
                                    {
                                        PubSessions pubSession = _context.GetPubSession(Session); //get session info
                                        if (pubSession != null) //session exists
                                        {
                                            if (pubSession.OrderBasketId != 0)
                                            {
                                                //add item 
                                                OrderBasketID = pubSession.OrderBasketId;
                                                PubOrderBasketItems item = _context.GetPubOrderBasketItem(pubSession.OrderBasketId, id);
                                                if (item != null)
                                                {
                                                    //edit item
                                                    int newQuantity = quantity + item.ItemQuantity;
                                                    if (newQuantity <= Item.ItemStock)
                                                    {
                                                        if (newQuantity <= GlobalConstants.MaxItemsPerOrder)
                                                        {
                                                            //edit
                                                            if (_context.EditPubOrderBasketItem(OrderBasketID, id, newQuantity))
                                                            {
                                                                return PartialView("ToastMessage", ToastAlert.Toast("Item Added", String.Format("{0}x {1} was added to your basket. Total Quantity: {2}", quantity, Item.ItemName, newQuantity), Item.ItemImagePath));
                                                            }
                                                            else
                                                            {
                                                                return PartialView("ToastMessage", ToastAlert.DefaultError());
                                                                //error item could not be updated
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //max items error
                                                            return PartialView("ToastMessage", ToastAlert.ItemQuantityMaxError(Item.ItemImagePath));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return PartialView("ToastMessage", ToastAlert.ItemQuantityStockError(Item.ItemImagePath));
                                                        //not enoguh in stock error
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //create new orderbasket and add item
                                                OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                                if (pubSession.UserId != 0)
                                                {
                                                    if (_context.GetPubUser(pubSession.UserId) != null)
                                                    {
                                                        _context.UpdatePubUserOrderBasket(pubSession.UserId, OrderBasketID);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //create a new session and orderbasket and add item
                                            Session = _context.AddPubSession(null, null);
                                            if (Session != null)
                                            {
                                                HttpContext.Session.SetString("PubSession", Session);
                                                OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                            }
                                        }
                                    }
                                    else //create a new session and orderbasket and add item
                                    {
                                        Session = _context.AddPubSession(null, null);
                                        if (Session != null)
                                        {
                                            HttpContext.Session.SetString("PubSession", Session);
                                            OrderBasketID = _context.CreatePubSessionOrderBasket(Session, null);
                                        }
                                    }

                                    if (OrderBasketID != 0)
                                    {
                                        if (_context.AddPubOrderBasketItem(OrderBasketID, id, quantity))
                                        {
                                            return PartialView("ToastMessage", ToastAlert.Toast("Item Added", String.Format("{0}x {1} was added to your basket", quantity, Item.ItemName), Item.ItemImagePath));
                                        }
                                    }
                                }
                                else
                                {
                                    return PartialView("ToastMessage", ToastAlert.ItemQuantityMaxError(Item.ItemImagePath));
                                }


                            }
                            else
                            {
                                return PartialView("ToastMessage", ToastAlert.ItemQuantityStockError(Item.ItemImagePath));
                            }
                        }
                        else
                        {
                            //error item not on sale
                        }
                    }
                }
            }
            return PartialView("ToastMessage", ToastAlert.DefaultError());
        }


        //            if (quantity < Item.ItemStock) {
        //                if (quantity < GlobalConstants.MaxItemsPerOrder)
        //                {
        //                    var OrderItems = HttpContext.Session.GetString("OrderItems");
        //                    List<PubOrderItems> PubOrderItems;
        //                    if (OrderItems != null) //an order basket already exists
        //                    {
        //                        try
        //                        {
        //                            PubOrderItems = JsonSerializer.Deserialize<List<PubOrderItems>>(OrderItems); //parse the existing basket
        //                            foreach (PubOrderItems item in PubOrderItems)
        //                            {
        //                                if (item.ItemId == id)
        //                                {
        //                                    int newQuantity = item.ItemQuantity + quantity;
        //                                    if (newQuantity < GlobalConstants.MaxItemsPerOrder)
        //                                    {
        //                                        if (newQuantity < Item.ItemStock)
        //                                        {
        //                                            item.ItemQuantity = newQuantity;
        //                                            HttpContext.Session.SetString("OrderItems", JsonSerializer.Serialize(PubOrderItems));
        //                                            return PartialView("ToastMessage", ToastAlert.Toast("Item Added", String.Format("{0}x {1} was added to your basket. Total Quantity: {2}", quantity, Item.ItemName, newQuantity), Item.ItemImagePath));
        //                                        }
        //                                        else
        //                                        {
        //                                            return PartialView("ToastMessage", ToastAlert.ItemQuantityStockError());
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        return PartialView("ToastMessage", ToastAlert.ItemQuantityMaxError());
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        catch
        //                        {
        //                            PubOrderItems = new List<PubOrderItems>(); //if the json cannot be parsed then the session string was corrupt so create a new list.
        //                        }
        //                    }
        //                    else //create new order basket
        //                    {
        //                        PubOrderItems = new List<PubOrderItems>(); //create new basket
        //                    }

        //                    //Update Order Basket session!
        //                    PubOrderItems.Add(new PubOrderItems() { ItemId = id, ItemQuantity = quantity });
        //                    HttpContext.Session.SetString("OrderItems", JsonSerializer.Serialize(PubOrderItems));

        //                    return PartialView("ToastMessage", ToastAlert.Toast("Item Added", string.Format("{0}x {1} was added to your basket", quantity, Item.ItemName), Item.ItemImagePath ?? GlobalConstants.DefaultImagePath));
        //                }
        //                else
        //                {
        //                    return PartialView("ToastMessage", ToastAlert.ItemQuantityMaxError());
        //                }
        //            }
        //            else
        //            {
        //                return PartialView("ToastMessage", ToastAlert.ItemQuantityStockError());


        [HttpGet]
        public IActionResult OrderBasket() //
        {
            var Session = HttpContext.Session.GetString("PubSession");
            if (Session != null)
            {
                PubSessions pubSession = _context.GetPubSession(Session); //get session info
                if (pubSession != null) //session exists
                {
                    if (pubSession.OrderBasketId != 0) //if basket exists
                    {
                        List<OrderBasketViewModel> BasketItemList = _context.GetPubOrderBasketItems(pubSession.OrderBasketId);
                        if (BasketItemList != null) //if fetched list of items in basket
                        {
                            return View(BasketItemList);
                        }
                    }
                }
            }
            return View();





            //var OrderItems = HttpContext.Session.GetString("OrderItems");
            //if (OrderItems != null)
            //{
            //    List<PubOrderItems> PubOrderItems;
            //    try
            //    {
            //        PubOrderItems = JsonSerializer.Deserialize<List<PubOrderItems>>(OrderItems);
            //    }
            //    catch
            //    {
            //        HttpContext.Session.Remove("OrderItems"); //error: orderitems did not contian a list of pub items, reset the session
            //        return View();
            //    }

            //    List<OrderBasketViewModel> OrderBasketList = new List<OrderBasketViewModel>();

            //    foreach (PubOrderItems item in PubOrderItems)
            //    {
            //        PubItems PubItem = _context.GetPubItem(item.ItemId);
            //        if (PubItem != null)
            //        {
            //            OrderBasketList.Add(new OrderBasketViewModel() { ItemQuantity = item.ItemQuantity, PubItem = PubItem });
            //        }
            //    }
            //    return View(OrderBasketList);
            //}
            //return View();
        }

        //[HttpPost]
        //public IActionResult Order()                                    // PUB ORDER CREATION GOES HERE
        //{
        //    var userstring = HttpContext.Session.GetString("User");
        //    if (userstring != null)
        //    {
        //        PubUsers User = JsonSerializer.Deserialize<PubUsers>(userstring);
        //        var OrderItems = HttpContext.Session.GetString("OrderItems");
        //        if (OrderItems != null)
        //        {
        //            List<PubOrderItems> PubOrderItems;
        //            try
        //            {
        //                PubOrderItems = JsonSerializer.Deserialize<List<PubOrderItems>>(OrderItems);
        //            }
        //            catch
        //            {
        //                HttpContext.Session.Remove("OrderItems"); //error: orderitems did not contian a list of pub items, reset the session
        //                return RedirectToAction("OrderBasket");
        //            }

        //            bool valid = true;
        //            foreach (PubOrderItems item in PubOrderItems)
        //            {
        //                PubItems pubItem = _context.GetPubItem(item.ItemId);
        //                if (pubItem != null)
        //                {
        //                    if (pubItem.ItemOnSale)
        //                    {
        //                        if (item.ItemQuantity <= pubItem.ItemStock)
        //                        {

        //                        }
        //                    }
        //                }
        //            }
        //            if (valid)
        //            {
        //                int OrderID = _context.AddPubOrder(User.UserId);
        //                if (OrderID > 0)
        //                {
        //                    foreach (PubOrderItems item in PubOrderItems)
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return RedirectToAction("OrderBasket");
        //}

        [HttpPost]
        public IActionResult Create(CreateItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string UniqueFileName = ProcessUploadedFile(model);

                PubItems newItem = _context.AddPubItem(new PubItems()
                {
                    ItemName = model.ItemName,
                    ItemType = model.ItemType,
                    ItemPrice = model.ItemPrice,
                    ItemImagePath = UniqueFileName,
                    ItemDescription = model.ItemDescription,
                    ItemOnSale = model.ItemOnSale,
                    ItemStock = model.ItemStock,

                });
                if (!(newItem == null))
                {
                    return RedirectToAction("Details", new { id = newItem.ItemId });
                }
                else
                {
                    ModelState.AddModelError("", "An error occured, could not create a new item.");
                    //ERROR: item was not added
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                PubItems EditedItem = new PubItems()
                {
                    ItemId = model.ItemID,
                    ItemDescription = model.ItemDescription,
                    ItemName = model.ItemName,
                    ItemOnSale = model.ItemOnSale,
                    ItemPrice = model.ItemPrice,
                    ItemStock = model.ItemStock,
                    ItemType = model.ItemType,
                    ItemImagePath = model.ItemExistingImagePath
                    };

                if (model.ItemImage != null)
                {
                    if (model.ItemExistingImagePath != null)
                    {
                        string existingFilePath = Path.Combine(webHostEnvironment.WebRootPath, "imgs", model.ItemExistingImagePath);
                        System.IO.File.Delete(existingFilePath);
                    }
                    EditedItem.ItemImagePath = ProcessUploadedFile(model);
                }

                //int result = _context.AddPubItem();

                if (_context.EditPubItem(EditedItem) == true)
                {
                    return RedirectToAction("Details", new { id = EditedItem.ItemId });
                }
                else
                {
                    ModelState.AddModelError("", "An error occured, could not create a new item.");
                    //ERROR: item was not added
                }
            }
            return View(model);
        }

        private string ProcessUploadedFile(CreateItemViewModel model)
        {
            string UniqueFileName = null;
            if (!(model.ItemImage == null))
            {
                string imgsFolder = Path.Combine(webHostEnvironment.WebRootPath, "imgs");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + model.ItemImage.FileName; //class that can create a string of guarenteed unique characters (an unique id)
                string FilePath = Path.Combine(imgsFolder, UniqueFileName);
                using var fileStream = new FileStream(FilePath, FileMode.Create);
                model.ItemImage.CopyTo(fileStream);

                //using (var fileStream = new FileStream(FilePath, FileMode.Create))
                //{
                //    model.ItemImage.CopyTo(fileStream);
                //}

            }

            return UniqueFileName;
        }
    }
}