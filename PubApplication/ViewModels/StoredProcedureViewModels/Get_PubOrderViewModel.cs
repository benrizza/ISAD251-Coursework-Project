using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels.StoredProcedureViewModels
{
    public class Get_PubOrderViewModel
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotalCost { get; set; }
    }
}
