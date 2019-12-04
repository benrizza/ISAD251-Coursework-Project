using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubApplication.Models;
using PubApplication.Models.Enum;
using PubApplication.ViewModels;
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
                var user = new PubUsers { UserFirstName = model.UserFirstName, UserLastName = model.UserLastName, UserPassword = model.UserPassword, UserAccessRank = UserAccessRank.Customer };
                int result = _context.AddPubUser(user);
                if (!(result == 0))
                {    //User created, ID stored in result
                    user.UserId = result;
                    HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));
                    ViewBag.UserID = result;
                    return View("RegistrationSuccess");
                } 
                else {
                    ModelState.AddModelError("","Could not create a new account.");
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
                            HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));
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
            }
            ModelState.AddModelError("Invalid Entry", "Either the User ID or Password are incorrect.");
            ViewBag.returnController = returnController;
            ViewBag.returnAction = returnAction;
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout(string returnController, string returnAction)
        {
            HttpContext.Session.Remove("User");
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