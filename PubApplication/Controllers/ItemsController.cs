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
            PubItemsViewModel model = null;

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
                return View(new FilterItemsViewModel() { ItemName = ItemName, ItemOnSale = ItemOnSale ?? true, ItemType = ItemType ?? null, PageNumber = PageNumber ?? 0 });
            }
            else
            {
                ViewBag.PubItemsViewModel = null;
                return View(new FilterItemsViewModel() { ItemName = ItemName, ItemOnSale = ItemOnSale ?? true, ItemType = ItemType ?? null, PageNumber = PageNumber ?? 0 });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!(HttpContext.Session.GetString("User") == null))
            {
                var user = JsonSerializer.Deserialize<PubUsers>(HttpContext.Session.GetString("User"));
                if (user.UserAccessRank == Models.Enum.UserAccessRank.Admin)
                {
                    return View();
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
                    return View(Item);
                }
            }
            return RedirectToAction("Index", "Home");
        }

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