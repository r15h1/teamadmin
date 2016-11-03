using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Team
    {
        public int ClubId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public bool? Deleted { get; set; }
    }
}
