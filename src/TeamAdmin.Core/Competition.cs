using System;
namespace TeamAdmin.Core
{
    public class Competition
    {
        public long? CompetitionId { get; set; }
        public string Name { get; set; }       
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}