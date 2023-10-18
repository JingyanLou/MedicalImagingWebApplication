using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIT5032_MyProject.Models;
using FIT5032_MyProject.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FIT5032_MyProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BulkEmailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BulkEmails
        public ActionResult Index()
        {
            // Fetch all users to display them to the admin
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var allUsers = userManager.Users.ToList();

            return View(allUsers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmails(FormCollection form)
        {
            // Fetch subject and content input by admin
            string subject = form["EmailSubject"];
            string content = form["EmailContent"];

            // Get list of email addresses
            var selectedUsers = form["SelectedUsers"];

            if (string.IsNullOrEmpty(selectedUsers))
            {
                ViewBag.Error = "Please select at least one user.";
                return RedirectToAction("Index");
            }

            var userEmails = selectedUsers.Split(',').ToList();

            EmailSender es = new EmailSender();

            try
            {
                foreach (var email in userEmails)
                {
                    es.Send(email, subject, content);
                }
                ViewBag.Result = "Bulk email sent successfully.";
            }
            catch
            {
                ViewBag.Error = "An error occurred while sending the emails.";
            }

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
