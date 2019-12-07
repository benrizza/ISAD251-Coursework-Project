using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class FilterItemsViewModel
    {
        [MaxLength(100, ErrorMessage = "An item name cannot exceed 100 characters")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Type")]
        public ItemTypes? ItemType { get; set; } = null;

        [Display(Name = "Item On Sale")]
        public bool ItemOnSale { get; set; } = true;

        public int PageNumber { get; set; } = 0; //page numbers always start at 0 :)
    }
}
