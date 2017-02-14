using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Player
    {
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }
        public byte? SquadNumber { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string ContactInfo { get; set; }
        public bool? Deleted { get; set; }
        public Team Team { get; set; }
    }
}
