using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyProject.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_MyProject.Controllers
{
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[Authorize]
        // GET: Branches
        public ActionResult Index()
        {
            return View(db.Branches.ToList());
        }

        [HttpPost]
        public ActionResult GiveRate(int Id, int Rate)
        {
            var userId = User.Identity.GetUserId();
            var rate = new Models.Rating
            {
                BranchId = Id,
                PatientUserId = userId,
                rate = Rate
            };

            db.Ratings.Add(rate);//insert into database 
            db.SaveChanges();

            return RedirectToAction($"Details/{Id}");
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);

            if (branch == null)
            {
                return HttpNotFound();
            }

            //fetch the bookabletimeslots for that branch 
            var bookableTimeSlots = db.BookableTimeSlots.Where(b => b.BranchId == id.Value).ToList();
            ViewBag.BookableTimeSlots = bookableTimeSlots;

            branch.Ratings =db.Ratings.Where(r=>r.BranchId==id).ToList();

            //calculate the avg rating
            // // Calculate the average rating for the branch
            if (branch.Ratings.Any())
            {
                branch.AverageRating = (decimal)branch.Ratings.Average(r => r.rate);
            }
            else
            {
                branch.AverageRating = 0;
            }

            return View(branch);
        }

        [Authorize(Roles = "Admin")]
        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Latitude,Longitude")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        [Authorize(Roles = "Admin")]
        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Latitude,Longitude")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        [Authorize(Roles = "Admin")]
        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
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
