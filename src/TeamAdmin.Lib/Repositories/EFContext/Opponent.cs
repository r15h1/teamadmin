using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Opponent
    {
        public int OpponentId { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Website { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
