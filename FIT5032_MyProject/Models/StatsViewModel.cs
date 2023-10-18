using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class StatsViewModel
    {
        public List<string> BranchNames { get; set; }
        public List<int> BookingCounts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

