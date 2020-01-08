using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PubApplication.Models
{
    public partial class PubOrderBasketItems
    {
        public int OrderBasketId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }

    }
}
