using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.ViewModels
{
    public class PubUserDetailsViewModel
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public UserAccessRank UserAccessRank { get; set; }
        public int UserOrderBasketID { get; set; }
    }
}
