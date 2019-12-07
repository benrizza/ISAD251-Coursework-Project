using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Models.Enum
{
    public enum ItemTypes
    {
        Drink,
        Snack
    }

    public static class PubItemType
    {
        public static ItemTypes GetItemType(string type)
        {
            if (type == ItemTypes.Snack.ToString())
            {
                return ItemTypes.Snack;
            }
            else //default to a pint (drink)
            {
                return ItemTypes.Drink;
            }
        }
    }
}
