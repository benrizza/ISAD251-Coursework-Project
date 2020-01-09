using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PubApplication.Models
{
    public partial class PubItems
    {
        public PubItems()
        {
            PubOrderItems = new HashSet<PubOrderItems>();
            //PubOrderBasketItems = new HashSet<PubOrderBasketItems>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public ItemTypes ItemType { get; set; }
        public decimal ItemPrice { get; set; }
        [JsonIgnore]
        public string ItemImagePath { get; set; }
        public string ItemDescription { get; set; }
        [JsonIgnore]
        public int ItemStock { get; set; }
        [JsonIgnore]
        public bool ItemOnSale { get; set; }

        [JsonIgnore]
        public virtual ICollection<PubOrderItems> PubOrderItems { get; set; }

        //public virtual ICollection<PubOrderBasketItems> PubOrderBasketItems { get; set; }
    }
}
