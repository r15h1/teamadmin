using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;

namespace TeamAdmin.Lib.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private IMapper mapper;

        public PlayerRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public bool DeletePlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public Core.Player GetPlayer(int playerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Core.Player> GetPlayers(int teamId)
        {
            var list = new List<Core.Player>();
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var players = context.Players.Where(p => (!p.Deleted.HasValue || !p.Deleted.Value) && p.TeamId == teamId).ToList();
                players.ForEach((p) => list.Add((mapper.Map<Core.Player>(p))));
                return list;
            }
        }

        public Core.Player SavePlayer(Core.Player player)
        {
            throw new NotImplementedException();
        }
    }
}
