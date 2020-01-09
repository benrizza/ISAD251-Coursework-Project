using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PubApplication.Models;

namespace PubApplication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemRequestsController : ControllerBase
    {
        private readonly ISAD251DBContext _context;

        public ItemRequestsController(ISAD251DBContext context)
        {
            _context = context;
        }

        // GET: api/ItemRequests
        [HttpGet]
        public ActionResult<IEnumerable<PubItems>> GetPubItems()
        {
            return _context.GetPubItems(null, true, 0).PubItemsList;
        }

        // GET: api/ItemRequests/5
        [HttpGet("{id}")]
        public ActionResult<PubItems> GetPubItems(int id)
        {
            var pubItems = _context.GetPubItem(id);

            if (pubItems == null)
            {
                return NotFound();
            }

            return pubItems;
        }
    }
}
