using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_MyProject.Models
{
    public class ImageMRI
    {
        public int id { get; set; }

        //report fk
        [Required]
        public int ReportId { get; set; }
        [ForeignKey("ReportId")]
        public virtual Report Report { get; set; }
    }
}