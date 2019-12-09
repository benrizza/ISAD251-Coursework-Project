using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class AddOrderItemViewModel
    {
        public int ItemId { get; set; }
        [Display(Name = "Quantity")]
        [Range(1, 100, ErrorMessage = "Quantity must be in the range of (1-100)")]
        public int ItemQuantity { get; set; } = 1;
    }
}
