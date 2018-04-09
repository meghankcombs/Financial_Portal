using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MKCFinancialPortal.Models;

namespace MKCFinancialPortal.Controllers
{
    public class InvitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invites
        public ActionResult Index()
        {
            var invites = db.Invites.Include(i => i.User).Include(i => i.Household);
            return View(invites.ToList());
        }

        // GET: Invites/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Invite invite = db.Invites.Find(id);
        //    if (invite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(invite);
        //}

        // GET: Invites/Create
        public ActionResult Create()
        {
            //ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: Invites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email")] Invite invite)
        {
            if (ModelState.IsValid)
            {
                //invite.HouseholdId = db.Households.Where(h => h.Id == invite.HouseholdId); OR get from create form...?
                //HHToken = generate here and associate with user's email
                invite.InviteDate = DateTime.Now;
                invite.InvitedById = HttpContext.User.Identity.GetUserId(); //Current logged in user

                //Add invite to db and save
                db.Invites.Add(invite);
                db.SaveChanges();

                //send invite link to user...
                //var from = "Meghan's Financial Portal <meghankcombs@gmail.com>";
                //var to = Email;
                //var inviteLink = link for user to click on to be invited to household
                //var email = new MailMessage(from, to)
                //{
                //    Subject = "Household Invitation from Meghan's Financial Portal",
                //    Body = "Click here to accept your inviation to " + HouseholdId.Name + ": " + inviteLink,
                //    IsBodyHtml = true
                //};

                //var svc = new PersonalEmail();
                //await svc.SendAsync(email);

                //invite.HasBeenUsed = true; //changed from false... how?

                return RedirectToAction("Index", "Home");
            }
            
            return View(invite);
        }

        // GET: Invites/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Invite invite = db.Invites.Find(id);
        //    if (invite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.InvitedById = new SelectList(db.Users, "Id", "FirstName", invite.InvitedById);
        //    ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
        //    return View(invite);
        //}

        // POST: Invites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,HouseholdId,Email,HHToken,InviteDate,InvitedById,HasBeenUsed")] Invite invite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(invite).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.InvitedById = new SelectList(db.Users, "Id", "FirstName", invite.InvitedById);
        //    ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invite.HouseholdId);
        //    return View(invite);
        //}

        //// GET: Invites/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Invite invite = db.Invites.Find(id);
        //    if (invite == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(invite);
        //}

        // POST: Invites/Delete/5
        // CANNOT REMOVE AN INVITE ONCE IT'S SENT
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Invite invite = db.Invites.Find(id);
            db.Invites.Remove(invite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
