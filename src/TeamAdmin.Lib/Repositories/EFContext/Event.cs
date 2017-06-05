using System;
using System.Collections.Generic;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Event
    {
        public Event()
        {
            ClubTeamEvents = new List<ClubTeamEvent>();
        }

        public long? EventId { get; set; }
        public byte EventType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ClubTeamEvent> ClubTeamEvents { get; set; }
        public string Address { get; set; }
        public int? OpponentId { get; set; }
        public Opponent Opponent { get; set; }
        public bool? Away { get; set; }
    }

    internal class ClubTeamEvent
    {        
        public long? ClubTeamEventId { get; set; }
        public int ClubId { get; set; }
        public int? TeamId { get; set; }
        public long? EventId { get; set; }
        public Event Event { get; set; }
        public Team Team { get; set; }
    }
}
