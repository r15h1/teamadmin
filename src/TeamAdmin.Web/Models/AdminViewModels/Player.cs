using System;
using System.Collections.Generic;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Player
    {
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }
        public int? SquadNumber { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public List<PlayerContactInfo> ContactInfo { get; set; }    
    }
}
