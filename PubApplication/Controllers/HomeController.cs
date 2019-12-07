﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PubApplication.Models;

namespace PubApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISAD251DBContext _context;

        public HomeController(ILogger<HomeController> logger, ISAD251DBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<PubItems> RandomItems = new List<PubItems>
            {
                _context.GetRandomPubItem(Models.Enum.ItemTypes.Drink),
                _context.GetRandomPubItem(Models.Enum.ItemTypes.Snack)
            };

            ViewBag.ViewItems = RandomItems;
            ViewBag.returnController = ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.returnAction = ControllerContext.RouteData.Values["action"].ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.returnController = ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.returnAction = ControllerContext.RouteData.Values["action"].ToString();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
