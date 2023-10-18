using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class Rating
    {
        public int id { get; set; }

        //1 star to 5 stars
        [Required]
        [Range(1, 5)]
        public int rate { get; set; }

        //branch 
        [Required]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }


        //patient submit a star to branch
        [Required]
        public string PatientUserId { get; set; } // FK to AspNetUsers
        [ForeignKey("PatientUserId")]
        public virtual ApplicationUser PatientUser { get; set; }

    }
}