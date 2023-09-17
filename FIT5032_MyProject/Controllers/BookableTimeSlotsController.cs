using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyProject.Models;

namespace FIT5032_MyProject.Controllers
{
    public class BookableTimeSlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookableTimeSlots
        public ActionResult Index()
        {
            return View(db.BookableTimeSlots.ToList());
        }

        // GET: BookableTimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Branch branch = db.Branches.Find(id);
            BookableTimeSlot bookableTimeSlot = db.BookableTimeSlots.Find(id);
            if (bookableTimeSlot == null)
            {
                return HttpNotFound();
            }
            return View(bookableTimeSlot);
        }

        // GET: BookableTimeSlots/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name");
            var DoctorRole = db.Roles.Where(r => r.Name == "Doctor").FirstOrDefault();
            var Doctors = db.Users.Where(u => u.Roles.Any(r => r.RoleId == DoctorRole.Id)).ToList();
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name");
            ViewBag.DoctorUserId = new SelectList(Doctors, "Id", "Firstname");

            return View();
        }

        // POST: BookableTimeSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,StartTime,EndTime,BranchId,DoctorUserId,IsAvailable")] BookableTimeSlot bookableTimeSlot)
        {
            if (ModelState.IsValid)
            {
                
                db.BookableTimeSlots.Add(bookableTimeSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookableTimeSlot);
        }


        // GET: BookableTimeSlot/Edit/5
        public ActionResult Edit(int? id)
        {
            //to be continued! need to add more stuffs in here 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Branch branch = db.Branches.Find(id);
            BookableTimeSlot bookableTimeSlot = db.BookableTimeSlots.Find(id);
            if (bookableTimeSlot == null)
            {
                return HttpNotFound();
            }
            return View(bookableTimeSlot);
        }

        // POST: BookableTimeSlot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,StartTime,EndTime,BranchId,DoctorUserId,IsAvailable")] BookableTimeSlot bookableTimeSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookableTimeSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookableTimeSlot);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookableTimeSlot bookableTimeSlot = db.BookableTimeSlots.Find(id);
            if (bookableTimeSlot== null)
            {
                return HttpNotFound();
            }
            return View(bookableTimeSlot);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookableTimeSlot bookableTimeSlot = db.BookableTimeSlots.Find(id);
            db.BookableTimeSlots.Remove(bookableTimeSlot);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





    }
}