using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class FilterOrdersViewModel
    {
        [Display(Name = "User ID")]
        public int? UserID { get; set; }

        public int PageNumber { get; set; } = 0; //page numbers always start at 0 :)
    }
}
