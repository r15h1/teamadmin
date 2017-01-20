using System;
using System.ComponentModel.DataAnnotations;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Event
    {
        public long? EventId { get; set; }

        [Required(ErrorMessage = "Please enter start date and time")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        [Required]
        public EventType EventType { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter end date and time")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
