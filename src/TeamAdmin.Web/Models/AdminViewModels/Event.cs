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
            OpponentList = new List<ApiViewModels.Opponent>();
        }

        public long? EventId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        //[CompareDateAttribute("Start date must be greater than current date and time")]
        public DateTime? StartDate { get; set; }

        public string Description { get; set; }

        [Required]
        public EventType EventType { get; set; }

        public string Title { get; set; }

        [Required]        
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public List<int> Teams { get; set; }
        public List<Team> TeamList{ get; set; }
        public string Address { get; set; }
        public int? Opponent { get; set; }
        public List<ApiViewModels.Opponent> OpponentList { get; set; }
        public bool Away { get; set; }
    }
}
