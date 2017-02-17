using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class EventsCalendarModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<string> EventTypes { get; set; }
    }
}
