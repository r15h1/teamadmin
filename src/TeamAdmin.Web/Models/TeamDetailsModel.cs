using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamAdmin.Web.Models
{
    public class TeamDetailsModel
    {
        public Core.Team Team { get; set; }
        public IEnumerable<IGrouping<DateTime, Core.Event>> Events { get; set; }
    }
}
