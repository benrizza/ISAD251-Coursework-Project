using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class EditItemViewModel : CreateItemViewModel
    {
        public int ItemID { get; set; }
        public string ItemExistingImagePath { get; set; }
    }
}
