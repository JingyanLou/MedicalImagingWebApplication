using FIT5032_MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FIT5032_MyProject.Controllers
{
    public class StatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stats
        public ActionResult BookingStats()
        {
            StatsViewModel model = new StatsViewModel();

            return View(model);
        }
        
        [HttpPost]
        public ActionResult BookingStats(DateTime? startDate, DateTime? endDate)
        {
            DateTime start = startDate ?? DateTime.Now.AddDays(-7);
            DateTime end = endDate ?? DateTime.Now;

            var stats = db.BookableTimeSlots
                   .Where(b => b.Date >= start && b.Date <= end)
                   .GroupBy(b => b.BranchId)
                   .Select(g => new { BranchId = g.Key, Count = g.Count() })
                   .ToList();

            var viewModel = new StatsViewModel
            {
                StartDate = start,
                EndDate = end,
                BranchNames = new List<string>(),
                BookingCounts = new List<int>()
            };

            foreach (var stat in stats)
            {
                viewModel.BranchNames.Add(db.Branches.Find(stat.BranchId).Name);
                viewModel.BookingCounts.Add(stat.Count);
            }

            return View(viewModel);
        }
    }
}
