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
        public IEnumerable<string> Images { get; set; }
        public IEnumerable<string> Uniforms { get; set; }
    }
}