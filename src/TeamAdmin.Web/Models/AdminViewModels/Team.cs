using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class Team
    {
        public int ClubId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? TeamId { get; set; }
    }
}
