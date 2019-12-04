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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.returnController = ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.returnAction = ControllerContext.RouteData.Values["action"].ToString();
            System.Diagnostics.Debug.WriteLine(ControllerContext.RouteData.Values["controller"].ToString());
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
