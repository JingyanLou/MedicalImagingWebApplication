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
        //patient can rate a branch!
        public int Id { set; get; }

        //rate the branch
        [Required]
        [Range(1,5)]
        public int star { set; get; }

        [Required]
        public string PatientUserId { get; set; } // FK to AspNetUsers
        [ForeignKey("PatientUserId")]
        public virtual ApplicationUser PatientUser { get; set; }

        //branch fk
        [Required]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

    }
}