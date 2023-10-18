using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FIT5032_MyProject.Models
{
    public class BulkEmailViewModels
    {
        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "Email Content")]
        public string Content { get; set; }

        // List of users to show on the page
        public List<UserEmail> Users { get; set; }

        // Constructor initializing the users list so we don't get a null reference exception
        public BulkEmailViewModels()
        {
            Users = new List<UserEmail>();
        }
    }

    public class UserEmail
    {
        public bool IsSelected { get; set; } // checkbox to select users

        public string Id { get; set; } // user's unique identifier

        [Display(Name = "User Email")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        // You can continue to add other fields if needed
    }
}
