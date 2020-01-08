using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels.StoredProcedureViewModels
{
    public class Get_PubOrderBasketItemsViewModel 
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemDescription { get; set; }
        public int ItemStock { get; set; }
        public bool ItemOnSale { get; set; }
        public int ItemQuantity { get; set; }
    }
}
