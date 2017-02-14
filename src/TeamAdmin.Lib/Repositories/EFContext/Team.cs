using System.Collections.Generic;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Team
    {
        public int ClubId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool? Deleted { get; set; }
        public ICollection<TeamMedia> TeamMedia { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
