using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Player
    {
        public int TeamId { get; set; }
        public int? PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhotoUrl { get; set; }
        public int? SquadNumber { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Province { get; set; }

        [MaxLength(6)]
        public string PostalCode { get; set; }
        public List<ContactInfo> ContactInfo { get; set; }    
    }
}
