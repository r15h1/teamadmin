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
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var player = context.Players.FirstOrDefault(p => p.PlayerId == playerId && (!p.Deleted.HasValue || !p.Deleted.Value));
                return mapper.Map<Core.Player>(player);
            }
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
            if (player.PlayerId.HasValue)
                return UpdatePlayer(player);

            return CreatePlayer(player);            
        }

        private Core.Player UpdatePlayer(Core.Player player)
        {
            if (IsDeleted(player)) return player;

            using (var context = ContextFactory.Create<ClubContext>())
            {
                var targetPlayer = mapper.Map<EFContext.Player>(player);   
                context.Entry(targetPlayer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return mapper.Map<Core.Player>(targetPlayer);
            }
        }

        private bool IsDeleted(Core.Player player)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var playerinfo = context.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId && (!p.Deleted.HasValue || !p.Deleted.Value));
                return playerinfo == null;
            }
        }

        private Core.Player CreatePlayer(Core.Player player)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var playerinfo = mapper.Map<EFContext.Player>(player);
                context.Players.Add(playerinfo);
                context.SaveChanges();
                return mapper.Map<Core.Player>(playerinfo);
            }
        }
    }
}
