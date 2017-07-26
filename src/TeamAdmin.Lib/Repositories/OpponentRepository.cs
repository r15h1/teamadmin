using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;

namespace TeamAdmin.Lib.Repositories
{
    public class OpponentRepository : IOpponentRepository
    {
        public Core.Opponent GetOpponent(int opponentId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.Opponents.FirstOrDefault(c => c.OpponentId == opponentId);
            }
        }

        public IEnumerable<Core.Opponent> GetOpponents(string name = "")
        {            
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return string.IsNullOrWhiteSpace(name) ? context.Opponents.ToList() : context.Opponents.Where(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant())).ToList();
            }
        }

        public Core.Opponent SaveOpponent(Core.Opponent opponent)
        {
            Ensure.NotNull(opponent);

            if (opponent.OpponentId.HasValue)
                return UpdateOpponent(opponent);

            return CreateOpponent(opponent);
        }

        private Core.Opponent CreateOpponent(Core.Opponent opponent)
        {            
            using (var context = ContextFactory.Create<ClubContext>())
            {
                context.Opponents.Add(opponent);
                context.SaveChanges();
                return opponent;
            }
        }

        private Core.Opponent UpdateOpponent(Core.Opponent opponent)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var opp = context.Opponents.FirstOrDefault(c => c.OpponentId == opponent.OpponentId);
                if (opp == null) throw new ArgumentException("This opponent does not exist.");

                opp.Name = opponent.Name;
                opp.Website = opponent.Website;
                opp.LogoUrl = opponent.LogoUrl;
                opp.ShortName = opponent.ShortName;

                context.Entry(opp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return opp;
            }
        }
    }
}