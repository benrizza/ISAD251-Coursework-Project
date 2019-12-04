using PubApplication.Models.Enum;
using System;
using System.Collections.Generic;

namespace PubApplication.Models
{
    public partial class PubUsers
    {
        public PubUsers()
        {
            PubOrders = new HashSet<PubOrders>();
        }

        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public UserAccessRank UserAccessRank { get; set; }
        public string UserPassword { get; set; }

        public virtual ICollection<PubOrders> PubOrders { get; set; }
    }
}
