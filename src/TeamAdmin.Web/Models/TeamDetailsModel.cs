using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class TeamDetailsModel
    {
        public Core.Team Team { get; set; }
        public IEnumerable<IGrouping<DateTime, Core.Event>> Events { get; set; }
        public IEnumerable<Player> Players { get; internal set; }
    }
}
