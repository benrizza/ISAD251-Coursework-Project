using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels.StoredProcedureViewModels
{
    public class Get_ItemPriceViewModel
    {
        [Key]
        public int ItemId { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
