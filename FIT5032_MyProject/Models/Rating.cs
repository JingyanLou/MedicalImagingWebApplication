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

        //1 star to 5 stars
        [Required]
        [Range(1,5)]
        public int rate { get; set; }

        //branch 
        [Required]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

    }
}