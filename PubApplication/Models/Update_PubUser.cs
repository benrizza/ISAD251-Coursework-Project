using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models
{
    public class Update_PubUser
    {
        public int UserID { get; set; }
        public int UserFirstName { get; set; }
        public int UserLastName { get; set; }
        public int UserAccessRank { get; set; }
    }
}
