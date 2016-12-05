using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class News
    {
        public int? ClubId { get; set; }

        [Required]
        public string Body { get; set; }        
        public long? PostId { get; set; }
        public Core.PostStatus PostStatus { get; set; }

        [Required]
        public string Title { get; set; }

        public IEnumerable<string> Images { get; set; }
    }
}
