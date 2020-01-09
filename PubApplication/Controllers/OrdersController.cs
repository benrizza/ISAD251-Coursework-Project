using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PubApplication.Models;
using PubApplication.ViewModels;
using PubApplication.ViewModels.StoredProcedureViewModels;

namespace PubApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ISAD251DBContext _context;

        public OrdersController(ISAD251DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? UserID, int? PageNumber) //get user id
        {
            var Session = HttpContext.Session.GetString("PubSession"); //user must be logged in to view orders
            if (Session != null)
            {
                PubSessions pubSession = _context.GetPubSession(Session); //get session info
                if (pubSession != null) //session exists
                {
                    if (pubSession.UserId > 0) //if a user is logged in...
                    {
                        PubUsers pubUser = _context.GetPubUser(pubSession.UserId);
                        if (pubUser != null) //if user exists
                        {
                            int? ID;
                            if (pubUser.UserAccessRank == Models.Enum.UserAccessRank.Admin)
                            {
                                if (UserID != null && UserID > 0)
                                {
                                    ID = UserID;
                                }
                                else
                                {
                                    ID = null;
                                }
                            }
                            else
                            {
                                ID = pubUser.UserId;
                            }
                            int pageNumber = PageNumber ?? 0;
                            ViewBag.UserAccessRank = pubUser.UserAccessRank;
                            ViewBag.PubOrdersViewModel = _context.GetPubOrders(ID, pageNumber);
                            return View(new FilterOrdersViewModel { UserID = ID, PageNumber = pageNumber });
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Order(int? id)
        {
            if (id != null && id > 0) //check that user has given an id and that the ID given is bigger than 0 (all ids are bigger than 0)
            {
                var Session = HttpContext.Session.GetString("PubSession"); //user must be logged in to view an order
                if (Session != null)
                {
                    PubSessions pubSession = _context.GetPubSession(Session); //get session info
                    if (pubSession != null) //session exists
                    {
                        if (pubSession.UserId > 0) //if a user is logged in...
                        {
                            PubUsers pubUsers = _context.GetPubUser(pubSession.UserId);
                            if (pubUsers != null)
                            {
                                Get_PubOrderViewModel OrderDetails = _context.GetPubOrder((int)id);
                                if (OrderDetails != null && (OrderDetails.UserId == pubUsers.UserId || pubUsers.UserAccessRank == Models.Enum.UserAccessRank.Admin)) //only logged in user and admins are allowed to view.
                                {
                                    OrderViewModel orderViewModel = new OrderViewModel
                                    {
                                        OrderDetails = OrderDetails,
                                        OrderItems = _context.GetPubOrderItems((int)id)
                                    };
                                    ViewBag.LoggedInUserID = pubUsers.UserId;
                                    ViewBag.UserAccessRank = pubUsers.UserAccessRank;
                                    return View(orderViewModel);
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
            if (id > 0) //all ids are bigger than 0
            {
                var Session = HttpContext.Session.GetString("PubSession"); //user must be logged in to view an order
                if (Session != null)
                {
                    PubSessions pubSession = _context.GetPubSession(Session); //get session info
                    if (pubSession != null) //session exists
                    {
                        if (pubSession.UserId > 0) //if a user is logged in...
                        {
                            PubUsers pubUsers = _context.GetPubUser(pubSession.UserId); //fetch logged in user....
                            if (pubUsers != null) //if user exists...
                            {
                                Get_PubOrderViewModel Order = _context.GetPubOrder(id);
                                if (Order != null)
                                {
                                    if (Order.UserId == pubUsers.UserId)
                                    {
                                        if (_context.RemovePubOrder(id))
                                        {
                                            ViewBag.RemovedOrderID = id;
                                            return View();
                                        }
                                    }
                                    return RedirectToAction("Order", new { id });
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Order()
        {
            var Session = HttpContext.Session.GetString("PubSession");
            if (Session != null)
            {
                PubSessions pubSession = _context.GetPubSession(Session); //get session info
                if (pubSession != null) //session exists
                {
                    if (pubSession.UserId > 0) //if user is logged in
                    {
                        if (pubSession.OrderBasketId > 0) //if basket exists
                        {
                            List<OrderBasketViewModel> BasketItemList = _context.GetPubOrderBasketItems(pubSession.OrderBasketId);
                            if (BasketItemList != null) //if fetched list of items in basket exists
                            {
                                foreach (OrderBasketViewModel basketitem in BasketItemList)
                                {
                                    if (basketitem.ItemQuantity > basketitem.PubItem.ItemStock)
                                    {
                                        TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.Toast("Error", "Item request quantity cannot exceede item stock.", basketitem.PubItem.ItemImagePath));
                                        return RedirectToAction("OrderBasket", "Items");
                                    }
                                }
                                int OrderID = _context.CreateOrder(pubSession.OrderBasketId, pubSession.UserId, pubSession.SessionId);
                                if (OrderID > 0)
                                {
                                    return RedirectToAction("Order", new { id = OrderID });
                                }
                                else
                                {
                                    TempData["ToastMessage"] = JsonSerializer.Serialize(ToastAlert.DefaultError());
                                }
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            return RedirectToAction("OrderBasket", "Items");
        }
    }
}