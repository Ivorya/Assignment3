using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SportDay.Models;
using SportDay.Units;

namespace SportDay.Controllers
{
    public class BookingsController : Controller
    {
        private SportDayModel db = new SportDayModel();

        // GET: Bookings
        [Authorize]

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var bookings = db.Bookings.Where(b => b.AspNetUser.Id == userId).Include(b => b.Event);
            return View(bookings.ToList());
        }

        public ActionResult Calendar()
        {
            return View();

        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create(string date)
        {

            string idd = User.Identity.GetUserId();
            var USER = db.AspNetUsers.Where(s => s.Id == idd).ToList();
            //var events = db.Events.Where(e => e.EventId == id).ToList();

            //Event ev = db.Events.Find(id);
            // DateTime start = ev.StartDate;
            // DateTime end = ev.EndDate;
            // List<DateTime> lista = new List<DateTime>();

            // foreach (DateTime day in EachDay(start, end)) { lista.Add(day)};

            ViewBag.UserId = new SelectList(USER, "Id", "Email");
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Name");

            if (null == date)
                return RedirectToAction("Index");

            Booking e = new Booking();
            DateTime convertedDate = DateTime.Parse(date);
            e.Date = convertedDate;
            ViewBag.Date = date;
            return View();
        }


        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,Info,UserId,EventId,Date")] Booking booking)
        {

            booking.UserId = User.Identity.GetUserId();
            var id = booking.EventId;
            Event ev = db.Events.Find(id);
            DateTime start = ev.StartDate;
            DateTime end = ev.EndDate;
            ModelState.Clear();
            TryValidateModel(booking);

            if (DateTime.Compare(DateTime.Now, end) > 0)
            {
                ModelState.AddModelError("", "Sorry, The Event was end.");
            }
            else if (DateTime.Compare(start, booking.Date) > 0 || DateTime.Compare(booking.Date, end) > 0)
            {
                ModelState.AddModelError("", "The date is not avilable.");

            }
            else if (ModelState.IsValid) {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string idd = User.Identity.GetUserId();
            var USER = db.AspNetUsers.Where(s => s.Id == idd).ToList();
            ViewBag.UserId = new SelectList(USER, "Id", "Email");
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Name", booking.EventId);
            
                String email = USER[0].Email;
                String subject = "Hi" + " "+ email;
                String contents = "You have successful booking the event:" + ev.Name;

                EmailSender es = new EmailSender();
                es.Send(email, subject, contents);


                return View(booking);
            
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            string idd = User.Identity.GetUserId();
            var USER = db.AspNetUsers.Where(s => s.Id == idd).ToList();
            ViewBag.UserId = new SelectList(USER, "Id", "Email", booking.UserId);

            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,Info,UserId,EventId,Date")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", booking.UserId);
            ViewBag.EventId = new SelectList(db.Events, "EventId", "Name", booking.EventId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
