using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class PubOrdersViewModel
    {
        public List<UserOrderViewModel> Orders { get; set; }
        public int RowCount { get; set; } //the total number of rows
    }
}
