using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class Event
    {
        public Event()
        {
            Teams = new List<Team>();
        }
        public long? EventId { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public string Title { get; set; }
        public DateTime EndDate { get; set; }
        public IList<Team> Teams { get; set; }
        public string Address { get; set; }
        public Opponent Opponent { get; set; }

    }

    public enum EventType
    {
        GAME = 1,
        TRAINING = 2,
        MEETING = 3,
        CELEBRATION = 4,
        OTHER = 5,
        EXHIBITION_GAME = 6
    }
}
