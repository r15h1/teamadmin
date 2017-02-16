using System.Collections.Generic;

namespace TeamAdmin.Web.Models.AdminViewModels
{
    public class PlayersListModel
    {
        public IEnumerable<Core.Player> Players { get; internal set; }
        public Core.Team Team { get; internal set; }
    }
}
