using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PubApplication.Models;

namespace PubApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ISAD251DBContext _context;

        public OrdersController(ISAD251DBContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            
            return View();
        }

        //[HttpGet]
        //public IActionResult Get
        //{

        //}




    }
}