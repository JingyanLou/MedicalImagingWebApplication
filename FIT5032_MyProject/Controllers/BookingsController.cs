using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyProject.Migrations;
using FIT5032_MyProject.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_MyProject.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Booking
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId(); //get current user id

            // Assuming you have a role named "Doctor" to identify doctor users.
            if (User.IsInRole("Doctor"))
            {
              var doctorBookings = db.Bookings
                .Include(b => b.BookableTimeSlot)
                .Include(b => b.BookableTimeSlot.Branch)
                .Include(b => b.PatientUser)  // Including patient info for the doctor to see
                .Where(b => b.BookableTimeSlot.DoctorUserId == userId).ToList();

                return View(doctorBookings);

               
            }
            else if(User.IsInRole("Patient"))
            {
                var userBookings = db.Bookings
                    .Include(b => b.BookableTimeSlot)
                    .Include(b => b.BookableTimeSlot.Branch)
                    .Include(b => b.BookableTimeSlot.DoctorUser)
                    .Where(b => b.PatientUserId == userId).ToList();

                return View(userBookings);
            }
            
                // Admin sees all bookings
                var allbookings = db.Bookings
                    .Include(b => b.BookableTimeSlot)
                    .Include(b => b.BookableTimeSlot.Branch)
                    .Include(b => b.BookableTimeSlot.DoctorUser)
                    .Include(b => b.PatientUser)
                    .ToList();
                return View(allbookings);
            
        }


        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var booking = db.Bookings.Find(id);

          
            return View(booking);
        }

        // GET: Booking/Create  
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookabletimeslot = db.BookableTimeSlots.Find(id);

            if(bookabletimeslot == null)
            {
                return HttpNotFound();

            }

            if (bookabletimeslot.IsAvailable)
            {
                var userId = User.Identity.GetUserId(); //get current user id 
                var booking = new Booking(); //created a new instance of booking 

                booking.PatientUserId = userId; //fill the patient id 
                booking.BookableTimeSlotId = bookabletimeslot.Id; // fill the bookabletimeslot id 

                //mark as booked
                db.BookableTimeSlots.AddOrUpdate(bookabletimeslot);
                bookabletimeslot.IsAvailable = false;

                //save chanegs to the database 
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound("The selected time slot is not available.");
            }

        }
            

        // POST: Booking/Create .. no use 
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("ListofAppoinments");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);

        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
         
            Booking booking = db.Bookings.Find(id);

            db.Bookings.Remove(booking);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
