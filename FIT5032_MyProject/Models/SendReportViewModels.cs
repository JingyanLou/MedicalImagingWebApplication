using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FIT5032_MyProject.Models
{
    public class SendReportViewModels
    {
        public int ReportId { get; set; } // To track which report is being sent

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ToEmail { get; set; }

        // You can add more fields as needed (e.g., Subject, Body, etc.) but for now I've just kept the recipient email.
    }
}
