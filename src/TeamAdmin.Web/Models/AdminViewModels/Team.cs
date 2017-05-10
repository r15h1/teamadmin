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
        public int? TeamId { get; set; }

        [Required]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<string> Images { get; set; } = new List<string>();
        public IList<string> Uniforms { get; set; } = new List<string>();
    }
}