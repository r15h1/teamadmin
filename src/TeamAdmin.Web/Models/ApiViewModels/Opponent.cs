using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.ApiViewModels
{
    public class Opponent
    {
        public int? OpponentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string ShortName { get; set; }

        [MaxLength(250)]
        [Url]
        public string Website { get; set; }

        [MaxLength(1000)]
        [Url]
        public string LogoUrl { get; set; }
    }
}
