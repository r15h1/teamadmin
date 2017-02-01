using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class HomePageModel
    {
        public IEnumerable<Post> News{ get; set; }
        public IEnumerable<IGrouping<DateTime, Event>> Events { get; set; }
    }
}
