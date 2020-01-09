using PubApplication.Models;
using PubApplication.ViewModels.StoredProcedureViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class OrderViewModel
    {
        public List<Get_PubOrderItemsViewModel> OrderItems { get; set; }
        public Get_PubOrderViewModel OrderDetails { get; set; }
    }
}
