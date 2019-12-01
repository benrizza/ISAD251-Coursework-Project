using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models
{
    public class Get_PubItems_OnSaleFilter
    {
        public string ItemName { get; set; }
        public Boolean ItemOnSale { get; set; }
        public string ItemType { get; set; }
    }
}
