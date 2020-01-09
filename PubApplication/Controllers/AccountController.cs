using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubApplication.Models;
using PubApplication.Models.Enum;
using PubApplication.ViewModels;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PubApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISAD251DBContext _context;

        public AccountController(ISAD251DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var Session = HttpContext.Session.GetString("PubSession"); //user must be logged in to view orders
            if (Session != null)
            {
                PubSessions pubSession = _context.GetPubSession(Session); //get session info
                if (pubSession != null) //session exists
                {
                    if (pubSession.UserId == 0) //if a user is logged in...
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            } 
            else
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                PubUsers user = _context.AddPubUser(new PubUsers
                {
                    UserFirstName = model.UserFirstName,
                    UserLastName = model.UserLastName,
                    UserPassword = model.UserPassword,
                    UserAccessRank = UserAccessRank.Customer
                });
                if (!(user == null))
                {    //User created, ID stored in result
                    ViewBag.LogInError = false;
                    //---------LOG IN USER---------
                    if (LogInUser(user) == false)
                    {
                        ViewBag.LogInError = true;
                    }
                    ViewBag.User = user;
                    return View("RegistrationSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "An error occured, could not create a new account.");
                    //ERROR, user not created
                }
            }
            return View(model);
        }

        private bool LogInUser(PubUsers User) {
            //string UniqueFileName = Guid.NewGuid().ToString();
            string SessionString = HttpContext.Session.GetString("PubSession");
            if (SessionString != null) //session already exists, put user in existing session
            {
                PubSessions Session = _context.GetPubSession(SessionString);
                if (Session != null)
                {
                    int OrderBasketID;
                    if (User.UserOrderBasketID > 0)
                    {
                        OrderBasketID = User.UserOrderBasketID;
                    }
                    else
                    {
                        OrderBasketID = Session.OrderBasketId;
                        if (_context.UpdatePubUserOrderBasket(User.UserId, OrderBasketID) == false)
                        {
                            return false;
                        }
                    }
                    if (_context.UpdatePubSession(SessionString, User.UserId, OrderBasketID))
                    {
                        return true;
                    }
                    else
                    {
                        //error
                        return false;
                    }

                }
            }
            //create session with user
            SessionString = _context.AddPubSession(User.UserId, User.UserOrderBasketID);
            if (SessionString != null)
            {
                HttpContext.Session.SetString("PubSession", SessionString);
                return true;
            }
            else
            {
                ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
                return false;
            }
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserPassword != null)
                {
                    PubUsers user;
                    user = _context.GetPubUser(model.UserID);
                    if (!(user == null)) //testing if username exists by attempting to fetch a password to compare
                    {
                        if (user.UserPassword == model.UserPassword) //test if password matches
                        {
                            if(LogInUser(user))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
                                return View(model);
                            }
                        }

                    }
                }
            }
            ModelState.AddModelError("Invalid Entry", "Either the User ID or Password are incorrect.");
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var SessionString = HttpContext.Session.GetString("PubSession");
            if (SessionString != null)
            {
                PubSessions pubSessions = _context.GetPubSession(SessionString);
                if (pubSessions != null)
                {
                    _context.RemovePubSession(pubSessions.SessionId);
                    HttpContext.Session.Remove("PubSession");
                }                                                                    
            }
            return RedirectToAction("Index", "Home");
        }
    }
}





//[HttpPost]
//public IActionResult Login(LoginViewModel model)
//{
//    if (ModelState.IsValid)
//    {
//        if (model.UserPassword != null)
//        {
//            PubUsers user;
//            user = _context.GetPubUser(model.UserID);
//            if (!(user == null)) //testing if username exists by attempting to fetch a password to compare
//            {
//                if (user.UserPassword == model.UserPassword) //test if password matches
//                {
//                    //string UniqueFileName = Guid.NewGuid().ToString();
//                    string SessionString = HttpContext.Session.GetString("PubSession");
//                    if (SessionString != null) //session already exists, put user in existing session
//                    {
//                        PubSessions Session = _context.GetPubSession(SessionString);
//                        if (Session != null)
//                        {
//                            int UserOrderBasketID;
//                            if (user.UserOrderBasketID == 0)
//                            {
//                                UserOrderBasketID = Session.OrderBasketId;
//                            }
//                            else
//                            {
//                                UserOrderBasketID = user.UserOrderBasketID;
//                            }
//                            if (_context.UpdatePubSession(SessionString, user.UserId, UserOrderBasketID))
//                            {
//                                return RedirectToAction("Index", "Home");
//                            }
//                            else
//                            {
//                                //error
//                                ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
//                                return View(model);
//                            }

//                        }
//                    }
//                    //create session with user
//                    SessionString = _context.AddPubSession(user.UserId, user.UserOrderBasketID);
//                    if (SessionString != null)
//                    {
//                        HttpContext.Session.SetString("PubSession", SessionString);
//                        return RedirectToAction("Index", "Home");
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
//                        return View(model);
//                    }
//                }

//            }
//        }
//    }
//    ModelState.AddModelError("Invalid Entry", "Either the User ID or Password are incorrect.");
//    return View(model);
//}

//[HttpPost]
//public IActionResult Register(RegisterViewModel model)
//{
//    if (ModelState.IsValid)
//    {

//        PubUsers user = _context.AddPubUser(new PubUsers 
//        {
//            UserFirstName = model.UserFirstName, 
//            UserLastName = model.UserLastName, 
//            UserPassword = model.UserPassword, 
//            UserAccessRank = UserAccessRank.Customer 
//        });
//        if (!(user == null))
//        {    //User created, ID stored in result
//            ViewBag.UserID = user.UserId;
//            //---------LOG IN USER---------

//            string SessionString = HttpContext.Session.GetString("PubSession");
//            if (SessionString != null) //session already exists, put user in existing session
//            {
//                PubSessions Session = _context.GetPubSession(SessionString);
//                if (Session != null)
//                {
//                    int UserOrderBasketID;
//                    if (user.UserOrderBasketID == 0)
//                    {
//                        UserOrderBasketID = Session.OrderBasketId;
//                    }
//                    else
//                    {
//                        UserOrderBasketID = user.UserOrderBasketID;
//                    }
//                    if (_context.UpdatePubSession(SessionString, user.UserId, UserOrderBasketID))
//                    {
//                        ViewBag.User = user;
//                        return View("RegistrationSuccess");
//                    }                          
//                }
//            }
//            //create session with user
//            SessionString = _context.AddPubSession(user.UserId, user.UserOrderBasketID);
//            if (SessionString != null)
//            {
//                HttpContext.Session.SetString("PubSession", SessionString);
//            }
//            return View("RegistrationSuccess",user);
//        } 
//        else {
//            ModelState.AddModelError("", "An error occured, could not create a new account.");
//        //ERROR, user not created
//        }
//    }
//    return View(model);
//}