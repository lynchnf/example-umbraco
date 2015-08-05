using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ContactUsModel
    {
        [Display(Name = "From Email")]
        [Required]
        [EmailAddress]
        public string ReplyToEmail { get; set; }

        [Display(Name = "To Department")]
        [Required]
        public int ToDepartmentId { get; set; }

        [Display(Name = "Subject")]
        [Required]
        public string Subject { get; set; }

        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}