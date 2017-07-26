using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;

namespace TeamAdmin.Lib.Repositories
{
    public class CompetitionsRepository : ICompetitionsRepository
    {
        public bool DeleteCompetition(long id)
        {
            throw new NotImplementedException();
        }

        public Competition GetCompetition(long competitionId)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.Competitions.FirstOrDefault(c => c.CompetitionId == competitionId);
            }
        }

        public IEnumerable<Competition> GetCompetitions(string name = "")
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return string.IsNullOrWhiteSpace(name) ? context.Competitions.ToList() : context.Competitions.Where(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant())).ToList();
            }
        }

        public Competition SaveCompetition(Competition competition)
        {
            Ensure.NotNull(competition);

            if (competition.CompetitionId.HasValue)
                return UpdateCompetition(competition);

            return CreateCompetition(competition);
        }

        private Competition CreateCompetition(Competition competition)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                context.Competitions.Add(competition);
                context.SaveChanges();
                return competition;
            }
        }

        private Competition UpdateCompetition(Competition competition)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var comp = context.Competitions.FirstOrDefault(c => c.CompetitionId == competition.CompetitionId);
                if (comp == null) throw new ArgumentException("This opponent does not exist.");

                comp.Name = competition.Name;
                comp.Description = competition.Description;
                comp.EndDate = competition.EndDate;
                comp.StartDate = competition.StartDate;

                context.Entry(comp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return comp;
            }
        }
    }
}
