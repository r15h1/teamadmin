using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core;

namespace TeamAdmin.Web.Models
{
    public class PlayersListModel
    {
        public IEnumerable<Player> Players { get; internal set; }
        public Team Team { get; internal set; }
    }
}
