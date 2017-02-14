using System;
using System.Collections.Generic;

namespace TeamAdmin.Core
{
    public class Player
    {
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }
        public byte? SquadNumber { get; set; }
        public string Position { get; set; }
        public Address Address { get; set; }
        public List<PlayerContactInfo> ContactInfo { get; set; }
    }
}
