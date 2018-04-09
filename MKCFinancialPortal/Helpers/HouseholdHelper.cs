using Microsoft.AspNet.Identity;
using MKCFinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace MKCFinancialPortal.Helpers
{
    public class HouseholdHelper
    {
        private static Household household = new Household();
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserHelper userHelper = new UserHelper();

        public bool IsUnique(string houseName) //check to see if household name is unique when creating it
        {
            ApplicationDbContext db = new ApplicationDbContext();
            foreach (Household house in db.Households.ToList())
            {
                if (house.Name == houseName)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddUserToHousehold(string userId, int householdId) //add new user to household, if he/she not in household already
        {
            if (!userHelper.InHousehold(userId, householdId))
            {
                Household household = db.Households.Find(householdId); //assigns entire record to Household, NOT just householdId
                var newUser = db.Users.Find(userId);

                household.Users.Add(newUser);
                db.Entry(household).State = EntityState.Modified; //modifies existing Household record
                db.SaveChanges();
            }
        }

        public void RemoveUserFromHousehold(string userId, int householdId) //remove user (including self) from Household
        {
            if (userHelper.InHousehold(userId, householdId))
            {
                Household household = db.Households.Find(householdId);
                var delUser = db.Users.Find(userId);

                household.Users.Remove(delUser);
                db.Entry(household).State = EntityState.Modified; //modifies existing Project record
                db.SaveChanges();
            }
        }
    }
}