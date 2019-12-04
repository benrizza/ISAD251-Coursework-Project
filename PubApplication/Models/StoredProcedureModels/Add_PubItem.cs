using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models
{
    public class Add_PubItem
    {
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public Decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public int ItemStock { get; set; }
        public Boolean ItemOnSale { get; set; }
    }
}
