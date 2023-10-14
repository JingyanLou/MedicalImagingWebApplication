using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyProject.Models;
using FIT5032_MyProject.Utils;
using Microsoft.AspNet.Identity;

namespace FIT5032_MyProject.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        // GET: Reports
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            // If user is a Doctor
            if (User.IsInRole("Doctor"))
            {
                var doctorReports = db.Reports
                    .Include(r => r.Booking)
                    .Include(r => r.Booking.BookableTimeSlot)
                    .Where(r => r.Booking.BookableTimeSlot.DoctorUserId == userId)
                    .ToList();

                return View(doctorReports);
            }

            // If user is a Patient
            else if (User.IsInRole("Patient"))
            {
                var patientReports = db.Reports
                    .Include(r => r.Booking)
                    .Include(r => r.Booking.BookableTimeSlot)
                    .Include(r => r.Booking.BookableTimeSlot.DoctorUser)
                    .Where(r => r.Booking.PatientUserId == userId)
                    .ToList();

                return View(patientReports);
            }

            // Admin sees all reports
            var allReports = db.Reports
                .Include(r => r.Booking)
                .Include(r => r.Booking.BookableTimeSlot)
                .Include(r => r.Booking.BookableTimeSlot.DoctorUser)
                .Include(r => r.Booking.PatientUser)
                .ToList();
            return View(allReports);
        }


        //get
        public ActionResult SendReport(int id)
        {
            // Check if the report with the given ID exists
            var report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            // Initialize the ViewModel with the report ID
            var viewModel = new SendReportViewModels { ReportId = id };

            return View(viewModel);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendReport(SendReportViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var report = db.Reports.Find(model.ReportId);

                    if (report == null)
                    {
                        return HttpNotFound();
                    }

                    String toEmail = model.ToEmail;
                    String subject = "Your Report from Clinic";
                    String imagePath = Server.MapPath("~/Upload/" + report.ImagePath);

                    // Fetching the required details
                    String patientName = report.Booking.PatientUser.Firstname;
                    int bookingId = report.Booking.Id;
                    DateTime bookingDate = report.Booking.BookableTimeSlot.Date;
                    DateTime startTime = report.Booking.BookableTimeSlot.StartTime;
                    DateTime endTime = report.Booking.BookableTimeSlot.EndTime;
                    String bookingDoctor = report.Booking.BookableTimeSlot.DoctorUser.Firstname;

                    // Formatting the email content
                    String emailContent = $"Hello {patientName},\n\n" +
                                          $"Please find your report attached. Below are your booking details:\n\n" +
                                          $"Booking ID: {bookingId}\n" +
                                          $"Date: {bookingDate.ToShortDateString()}\n" +
                                          $"Time: {startTime.ToShortTimeString()} - {endTime.ToShortTimeString()}\n" +
                                          $"Doctor: {bookingDoctor}\n";


                    EmailSender es = new EmailSender();
                    es.SendWithAttachment(toEmail, subject, emailContent, imagePath); 

                    ViewBag.Result = "Email with report has been sent.";

                    ModelState.Clear();

                    return RedirectToAction("Index", "Reports"); // Redirecting back to the report list after sending the email
                }
                catch
                {
                    ViewBag.Error = "An error occurred while sending the email.";
                    return View(model);
                }
            }
            return View(model);
        }


        public ActionResult ShowImage(int id)
        {
            var report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }

            var imagePath = "~/Upload/" + report.ImagePath;
            return View((object)imagePath);
        }



        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {

            var currentUserId = User.Identity.GetUserId(); //get doctor id 

            // Fetch bookings related to the logged-in doctor

            var bookingsData = db.Bookings
                      .Where(b => b.BookableTimeSlot.DoctorUserId == currentUserId)
                      .ToList()  // Pulls the data into memory
                      .Select(b => new
                      {
                          Id = b.Id,
                          DisplayText = $"{b.BookableTimeSlot.Date.ToShortDateString()} {b.BookableTimeSlot.StartTime.ToShortTimeString()} - {b.PatientUser.Firstname}" // Assuming UserName stores the name of the patient
                      })
                      .ToList();

            ViewBag.BookingId = new SelectList(bookingsData, "Id", "DisplayText");



            //ViewBag.BookingId = new SelectList(bookingsForDoctor, "Id", "DisplayText"); //select the booking id but view the displayed text 



            //doctor will be able to see 
            // bookingid -> bookabletimeslotid -> to_get : appointment datetime , patient name
            // doctor selects a booking_id , 
            // he need to see the bookingdatetime
            // the patient name 

            // and upload a image to that booking 
            // doctor can only upload the image for his own booking 

            //for patient 
            // they can see their report for their booking

            //ViewBag.BookingId = new SelectList(db.Bookings, "Id", "PatientUserId");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImagePath,Title,BookingId")] Report report, HttpPostedFileBase postedFile)
        {

            // Check if the booking belongs to the logged-in doctor
            var currentUserId = User.Identity.GetUserId();
            var booking = db.Bookings.FirstOrDefault(b => b.Id == report.BookingId && b.BookableTimeSlot.DoctorUserId == currentUserId);


            if (booking == null)
            {
                // If booking doesn't belong to the logged-in doctor or doesn't exist
                ModelState.AddModelError("", "Invalid booking selected.");
                return View(report);
            }

            // Check if the booking belongs to the logged-in doctor
            var currentUserId = User.Identity.GetUserId();
            var booking = db.Bookings.FirstOrDefault(b => b.Id == report.BookingId && b.BookableTimeSlot.DoctorUserId == currentUserId);


            if (booking == null)
            {
                // If booking doesn't belong to the logged-in doctor or doesn't exist
                ModelState.AddModelError("", "Invalid booking selected.");
                return View(report);
            }

            ModelState.Clear();
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            report.ImagePath = myUniqueFileName;
            TryValidateModel(report);

            if (ModelState.IsValid)
            {
                //upload image 
                string serverPath = Server.MapPath("~/Upload/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = report.ImagePath + fileExtension;
                report.ImagePath = filePath;
                postedFile.SaveAs(serverPath + report.ImagePath); 

                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookingId = new SelectList(db.Bookings, "Id", "PatientUserId", report.BookingId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingId = new SelectList(db.Bookings, "Id", "PatientUserId", report.BookingId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase postedFile)
        {
            // Fetch the report from the database based on the ID
            var report = db.Reports.Find(id);

            if (report == null)
            {
                return HttpNotFound();
            }

            // Check if the booking belongs to the logged-in doctor
            var currentUserId = User.Identity.GetUserId();
            var booking = db.Bookings.FirstOrDefault(b => b.Id == report.BookingId && b.BookableTimeSlot.DoctorUserId == currentUserId);

            if (booking == null)
            {
                // If booking doesn't belong to the logged-in doctor or doesn't exist
                ModelState.AddModelError("", "Invalid booking selected.");
                return View(report);
            }

            // If a new image file is provided
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                // Create a unique filename
                var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = myUniqueFileName + fileExtension;

                // Save the uploaded file to the server
                string serverPath = Server.MapPath("~/Upload/");
                postedFile.SaveAs(serverPath + filePath);

                // Update the ImagePath in the report record
                report.ImagePath = filePath;
            }

            // Update the database and return to the index view
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // If the model isn't valid, return to the Edit view with the current report data
            ViewBag.BookingId = new SelectList(db.Bookings, "Id", "PatientUserId", report.BookingId);
            return View(report);
        }



        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
