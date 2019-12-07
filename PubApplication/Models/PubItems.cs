using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;

namespace PubApplication.Models
{
    public partial class PubItems
    {
        public PubItems()
        {
            PubOrderItems = new HashSet<PubOrderItems>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public ItemTypes ItemType { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemDescription { get; set; }
        public int ItemStock { get; set; }
        public bool ItemOnSale { get; set; }

        public virtual ICollection<PubOrderItems> PubOrderItems { get; set; }
    }
}
