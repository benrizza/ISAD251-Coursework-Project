using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PubApplication.Models;
using PubApplication.ViewModels.StoredProcedureViewModels;

namespace PubApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemPriceRequestController : ControllerBase
    {
        private readonly ISAD251DBContext _context;

        public ItemPriceRequestController(ISAD251DBContext context)
        {
            _context = context;
        }

        // GET: api/ItemRequests/5
        [HttpGet("{id}")]
        public ActionResult<Get_ItemPriceViewModel> GetPubItems(int id)
        {
            var pubItemPrice = _context.GetPubItemPrice(id, true); //get price of item - item must be on sale only find items with ItemOnSale as true.

            if (pubItemPrice == null)
            {
                return NotFound();
            }

            return pubItemPrice;
        }
    }
}