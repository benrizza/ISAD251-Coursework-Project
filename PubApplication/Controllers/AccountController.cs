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
            return View();
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
                    ViewBag.UserID = user.UserId;
                    //---------LOG IN USER---------

                    string SessionString = HttpContext.Session.GetString("PubSession");
                    if (SessionString != null) //session already exists, put user in existing session
                    {
                        PubSessions Session = _context.GetPubSession(SessionString);
                        if (Session != null)
                        {
                            int UserOrderBasketID;
                            if (user.UserOrderBasketID == 0 & Session.OrderBasketId != 0)
                            {
                                UserOrderBasketID = Session.OrderBasketId;
                            }
                            else
                            {
                                UserOrderBasketID = user.UserOrderBasketID;
                            }
                            _context.UpdatePubSession(SessionString, user.UserId, UserOrderBasketID);
                            return View("RegistrationSuccess",user);
                        }
                    }
                    //create session with user
                    SessionString = _context.AddPubSession(user.UserId, user.UserOrderBasketID);
                    if (SessionString != null)
                    {
                        HttpContext.Session.SetString("PubSession", SessionString);
                    }
                    return View("RegistrationSuccess",user);
                } 
                else {
                    ModelState.AddModelError("", "An error occured, could not create a new account.");
                //ERROR, user not created
                }
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnController, string returnAction)
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
                            //string UniqueFileName = Guid.NewGuid().ToString();
                            string SessionString = HttpContext.Session.GetString("PubSession");
                            if (SessionString != null) //session already exists, put user in existing session
                            {
                                PubSessions Session = _context.GetPubSession(SessionString);
                                if (Session != null)
                                {
                                    int UserOrderBasketID;
                                    if (user.UserOrderBasketID == 0 & Session.OrderBasketId != 0)
                                    {
                                        UserOrderBasketID = Session.OrderBasketId;
                                    }
                                    else
                                    {
                                        UserOrderBasketID = user.UserOrderBasketID;
                                    }
                                    if (_context.UpdatePubSession(SessionString, user.UserId, UserOrderBasketID))
                                    {
                                        if (returnController == null || returnAction == null)
                                        {
                                            return RedirectToAction("Index", "Home");
                                        }
                                        else
                                        {
                                            return RedirectToAction(returnAction, returnController);
                                        }
                                    }
                                    else
                                    {
                                        //error
                                        ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
                                        ViewBag.returnController = returnController;
                                        ViewBag.returnAction = returnAction;
                                        return View(model);
                                    }
                                    
                                }
                            }
                            //create session with user
                            SessionString = _context.AddPubSession(user.UserId, user.UserOrderBasketID);
                            if (SessionString != null)
                            {
                                HttpContext.Session.SetString("PubSession", SessionString);
                                if (returnController == null || returnAction == null)
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    return RedirectToAction(returnAction, returnController);
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ERROR", "An Error has occoured, you could not be logged in.");
                                ViewBag.returnController = returnController;
                                ViewBag.returnAction = returnAction;
                                return View(model);
                            }
                        }

                    }
                }
            }
            ModelState.AddModelError("Invalid Entry", "Either the User ID or Password are incorrect.");
            ViewBag.returnController = returnController;
            ViewBag.returnAction = returnAction;
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout(string returnController, string returnAction)
        {
            var SessionString = HttpContext.Session.GetString("PubSession");
            if (SessionString != null)
            {
                PubSessions pubSessions = _context.GetPubSession(SessionString);
                if (pubSessions != null & pubSessions.UserId > 0 )
                {
                    _context.UpdatePubSession(pubSessions.SessionId, 0, null);
                }                                                                    
            }
            if (returnController == null)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(returnAction, returnController);
            }
        }
    }
}