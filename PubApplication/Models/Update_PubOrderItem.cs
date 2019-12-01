using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models
{
    public class Update_PubOrderItem
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int ItemQuantity { get; set; }
    }
}
