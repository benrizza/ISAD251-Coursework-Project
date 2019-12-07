using PubApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class PubItemsViewModel
    {
        public List<PubItems> PubItemsList { get; set; } //collection of pub items gathered from a select
        public int RowCount { get; set; } //the total number of rows 
    }
}
