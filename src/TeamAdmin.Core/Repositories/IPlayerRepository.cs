using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core.Repositories
{
    public interface IPlayerRepository
    {
        Player SavePlayer(Player player);
        IEnumerable<Player> GetPlayers(int teamId);
        Player GetPlayer(int playerId);
        bool DeletePlayer(int teamId);
    }
}
