using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class HomePageModel
    {
        public IEnumerable<Post> News{ get; set; }
        public IEnumerable<IGrouping<DateTime, Event>> Games { get; set; }
        public IEnumerable<IGrouping<DateTime, Event>> TrainingSessions { get; set; }
    }
}
