using PubApplication.ViewModels.StoredProcedureViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class UserOrderViewModel
    {
        public List<Get_PubUserOrderItemsViewModel> OrderTopItems { get; set; }
        public Get_PubOrderViewModel OrderDetails { get; set; }
    }
}
