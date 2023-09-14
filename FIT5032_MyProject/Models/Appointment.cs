using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class Appointment
    {
        //pk
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }


        //branch fk
        [Required]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }


        //patient and staff fk 
        [Required]
        public string PatientUserId { get; set; }  // FK to AspNetUsers
        [Required]
        public string StaffUserId { get; set; }    // FK to AspNetUsers
        [ForeignKey("PatientUserId")]
        public virtual ApplicationUser PatientUser { get; set; }
        [ForeignKey("StaffUserId")]
        public virtual ApplicationUser StaffUser { get; set; }




    }
}