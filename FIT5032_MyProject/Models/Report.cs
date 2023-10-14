using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public string Title { get; set; }

        public int BookingId { get; set; }  // FK to Report
        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }





    }
}