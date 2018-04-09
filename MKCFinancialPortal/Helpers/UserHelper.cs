using Microsoft.AspNet.Identity;
using MKCFinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MKCFinancialPortal.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string UserFirstName() //logged in user's first name
        {
            if (HttpContext.Current.User != null)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var userFName = db.Users.FirstOrDefault(n => n.Id == userId).FirstName;
                return (userFName);
            }
            return "";
        }

        public static string UserLastName() //logged in user's last name
        {
            if (HttpContext.Current.User != null)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var userLName = db.Users.FirstOrDefault(n => n.Id == userId).LastName;
                return (userLName);
            }
            return "";
        }

        public bool InHousehold(string userId, int householdId) //Check if user is in household already
        {
            return db.Households.Find(householdId).Users.Any(u => u.Id == userId);
        }
    }
}