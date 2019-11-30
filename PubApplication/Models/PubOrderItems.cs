using System;
using System.Collections.Generic;

namespace PubApplication.Models
{
    public partial class PubOrderItems
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }

        public virtual PubItems Item { get; set; }
        public virtual PubOrders Order { get; set; }
    }
}
