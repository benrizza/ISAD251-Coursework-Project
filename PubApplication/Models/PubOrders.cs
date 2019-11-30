using System;
using System.Collections.Generic;

namespace PubApplication.Models
{
    public partial class PubOrders
    {
        public PubOrders()
        {
            PubOrderItems = new HashSet<PubOrderItems>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual PubUsers User { get; set; }
        public virtual ICollection<PubOrderItems> PubOrderItems { get; set; }
    }
}
