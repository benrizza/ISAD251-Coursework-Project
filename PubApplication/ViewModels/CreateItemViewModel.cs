using Microsoft.AspNetCore.Http;
using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class CreateItemViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "An item name cannot exceed 100 characters")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public ItemTypes ItemType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10, 2)")]
        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Item Image")]
        public IFormFile ItemImage { get; set; }

        [Required]
        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        [Display(Name = "Amount Of Item Stock")]
        public int ItemStock { get; set; }

        [Display(Name = "Item On Sale")]
        public bool ItemOnSale { get; set; }
    }
}
