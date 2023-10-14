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

       
    }
}
