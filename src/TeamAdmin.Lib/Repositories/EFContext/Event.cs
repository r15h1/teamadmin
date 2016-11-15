using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }

    internal class ClubTeamEvent
    {        
        public long? ClubTeamEventId { get; set; }
        public int ClubId { get; set; }
        public int? TeamId { get; set; }
    }
}
