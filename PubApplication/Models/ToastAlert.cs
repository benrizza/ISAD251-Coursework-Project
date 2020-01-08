using PubApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PubApplication.Models
{
    public static class ToastAlert
    {
        public static ToastAlertViewModel Toast(string Title, string Body, string ImagePath)
        {
            return new ToastAlertViewModel()
            {
                ToastBody = Body,
                ToastImagePath = ImagePath,
                ToastTitle = Title
            };
        }

        public static ToastAlertViewModel ItemQuantityStockError(string ImagePath)
        {
            return new ToastAlertViewModel() //ERROR - item could not be added to order basket
            {
                ToastBody = string.Format("Error: Item quantity cannot excede item stock."),
                ToastImagePath = ImagePath ?? GlobalConstants.DefaultImagePath,
                ToastTitle = "Error"
            };
        }

        public static ToastAlertViewModel ItemQuantityMaxError(string ImagePath)
        {
            return new ToastAlertViewModel() //ERROR - item could not be added to order basket
            {
                ToastBody = string.Format("Error: Quantity of a certian item cannot excede {0} per order.", GlobalConstants.MaxItemsPerOrder),
                ToastImagePath = ImagePath ?? GlobalConstants.DefaultImagePath,
                ToastTitle = "Error"
            };
        }

        public static ToastAlertViewModel DefaultError()
        {
            return new ToastAlertViewModel() //ERROR - item could not be added to order basket
            {
                ToastBody = string.Format("An error has occoured, the item request could not take place."),
                ToastImagePath = GlobalConstants.DefaultImagePath,
                ToastTitle = "Error"
            };
        } 
    }
}
