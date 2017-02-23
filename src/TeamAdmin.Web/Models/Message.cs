using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Web.Models
{
    public class Message
    {
        public long? MessageId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(150)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required(ErrorMessage = "Please click on the 'I\'m not a robot' box at the bottom of the page")]
        [FromForm(Name= "g-recaptcha-response")]
        public string ReCaptcha { get; set; }
    }
}