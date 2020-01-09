using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PubApplication.Models
{
    public partial class PubSessions
    {
        [Key]
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public int OrderBasketId { get; set; }
    }
}
