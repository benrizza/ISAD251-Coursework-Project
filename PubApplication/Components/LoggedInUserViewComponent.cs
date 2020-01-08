using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PubApplication.Models;
using PubApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PubApplication.Components
{
    public class LoggedInUserViewComponent : ViewComponent
    {
        private readonly ISAD251DBContext _context;

        public LoggedInUserViewComponent(ISAD251DBContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            string SessionString = HttpContext.Session.GetString("PubSession");
            if (SessionString != null) //session already exists, put user in existing session
            {
                PubSessions Session = _context.GetPubSession(SessionString);
                if (Session != null && Session.UserId > 0)
                {
                    PubUserDetailsViewModel UserDetails = _context.GetPubUserDetails(Session.UserId);
                    if (UserDetails != null)
                    {
                        ViewBag.UserDetails = UserDetails;
                        return View();
                    }
                }
            }
            return View();
        }
    }
}
