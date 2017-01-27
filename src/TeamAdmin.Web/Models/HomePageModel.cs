using System.Collections.Generic;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class HomePageModel
    {
        public IEnumerable<Post> News{ get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
