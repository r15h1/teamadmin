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
            Teams = new List<int>();
        }
        public long? EventId { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public EventType EventType { get; set; }
        public string Title { get; set; }
        public DateTime EndDate { get; set; }
        public IList<int> Teams { get; set; }
    }

    public enum EventType
    {
        GAME = 1,
        TRAINING = 2,
        MEETING = 3,
        CELEBRATION = 4,
        OTHER = 5
    }
}
