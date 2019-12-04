using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models.Enum
{
    public enum UserAccessRank
    {
        Customer,
        Admin
    }

    public static class UserRank{
        public static UserAccessRank GetRank(string thing)
        {
            if (thing == UserAccessRank.Admin.ToString())
            {
                return UserAccessRank.Admin;
            }
            else //default to being a customer
            {
                return UserAccessRank.Customer;
            }
        }
    }
}
