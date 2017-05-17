using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Event
    {
        public Event()
        {
            TeamList = new List<Team>();
        }

        public long? EventId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[CompareDateAttribute("Start date must be greater than current date and time")]
        public DateTime? StartDate { get; set; }

        public string Description { get; set; }

        [Required]
        public EventType EventType { get; set; }

        //[Required]
        public string Title { get; set; }

        [Required]
        //[CompareDateAttribute("Start date must be greater than current date and time, End date must be greater than start date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public List<int> Teams { get; set; }

        public List<Team> TeamList{ get; set; }

        public string Address { get; set; }

        public string Opponent { get; set; }
    }
}
