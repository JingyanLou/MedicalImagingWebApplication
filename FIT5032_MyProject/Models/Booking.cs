using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int BookableTimeSlotId { get; set; } // FK to BookableTimeSlot
        [ForeignKey("BookableTimeSlotId")]
        public virtual BookableTimeSlot BookableTimeSlot { get; set; }

        [Required]
        public string PatientUserId { get; set; } // FK to AspNetUsers
        [ForeignKey("PatientUserId")]
        public virtual ApplicationUser PatientUser { get; set; }

    }
}
