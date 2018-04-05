using Microsoft.AspNet.Identity;
using MKCFinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MKCFinancialPortal.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string UserFirstName()
        {
            if (HttpContext.Current.User != null)
            {
                var userId = HttpContext.Current.User.Identity.GetUserId();
                var userFName = db.Users.FirstOrDefault(n => n.Id == userId).FirstName;
                return (userFName);
            }
            return "";
        }
    }
}