using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PubApplication.Models;
using PubApplication.ViewModels;

namespace PubApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISAD251DBContext _context;

        public HomeController(ISAD251DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<PubItems> RandomItems;
            PubItems randomSnack = _context.GetRandomPubItem(Models.Enum.ItemTypes.Snack, true);  //get random item of type drink that is on sale.
            PubItems randomDrink = _context.GetRandomPubItem(Models.Enum.ItemTypes.Drink, true);

            if (randomSnack != null || randomDrink != null)
            {
                RandomItems = new List<PubItems> { randomDrink, randomSnack };
            }
            else
            {
                RandomItems = null;
            }

            ViewBag.ViewItems = RandomItems;
            return View(new AddOrderItemViewModel());
        }
    }
}
