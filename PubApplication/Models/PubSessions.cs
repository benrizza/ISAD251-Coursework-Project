using System;
using System.Collections.Generic;

namespace PubApplication.Models
{
    public partial class PubSessions
    {
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public int OrderBasketId { get; set; }
    }
}
