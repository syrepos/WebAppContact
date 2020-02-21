using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactWebApp.Models;
using Microsoft.AspNet.Identity;

namespace ContactWebApp.Controllers
{

    [Authorize]
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string _userId;
        
        
        
        // GET: Contacts

        public ActionResult Index()
        {
            _userId = GetCurrentUserId();
            var contacts = db.Contacts.Include(c => c.State)
                                    .Include(c => c.User)
                                    .Where(x=>x.UserId == _userId);
            return View(contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            _userId = GetCurrentUserId();
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
        //    ViewBag.UserId = _userId;
            return View();
        }

        // POST: Contacts/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,Birthday,StreetAdress1,StreetAdress2,City,StateId,Zip,UserId")] Contact contact)
        {
            _userId = GetCurrentUserId();
            contact.UserId = _userId;
            ModelState.Clear();
            TryValidateModel(contact);

            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
           //We don't need to pass the user ID in here.
            // ViewBag.UserId = _userId;
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            _userId = GetCurrentUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
       //     ViewBag.UserId = _userId;
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,PhonePrimary,PhoneSecondary,Birthday,StreetAdress1,StreetAdress2,City,StateId,Zip,UserId")] Contact contact)
        {
            _userId = GetCurrentUserId();
            var existing = db.Contacts.AsNoTracking().FirstOrDefault(x => x.Id == contact.Id && x.UserId == _userId);
            if (existing == null) return HttpNotFound();
            contact.UserId = _userId;
            ModelState.Clear();
            TryValidateModel(contact);
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", contact.StateId);
            ViewBag.UserId = _userId;
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var existing = db.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == _userId);
            if (existing == null)
            {
                return HttpNotFound();
            }
            return View(existing);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.FirstOrDefault(x=>x.Id ==id && x.UserId == _userId);
            db.Contacts.Remove(contact);
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

        protected string GetCurrentUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}
