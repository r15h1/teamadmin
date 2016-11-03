using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class Team
    {
        public Team(int clubId)
        {
            ClubId = clubId;
        }

        public int ClubId { get; private set; }
        public string Name { get; set; }
        public int? TeamId { get; set; }
    }
}
