using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class Branch
    {
        //pk
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        //rating  list 
        public List<Rating> Ratings { get; set; }
        
        //avgrating
        public decimal AvgRating { get; set; }

    }
}