using System;
using System.Collections.Generic;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Competition
    {
        public long? CompetitionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}