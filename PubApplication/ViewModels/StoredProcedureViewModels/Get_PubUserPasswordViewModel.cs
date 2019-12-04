using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels.StoredProcedureViewModels
{
    public class Get_PubUserPasswordViewModel
    {
        [Key]
        public int UserID { get; set; }
        public string UserPassword { get; set; }
    }
}
