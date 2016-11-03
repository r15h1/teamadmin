using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        IMapper mapper;
        public TeamRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public IEnumerable<Core.Team> Get()
        {
            var list = new List<Core.Team>();
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var teams = context.Teams.ToList();
                teams.ForEach((t) => list.Add(MapTeamFromDB(t)));
                return list;
            }
        }

        public Core.Team Save(Core.Team team)
        {
            if (team.TeamId.HasValue)
                return Update(team);

            return Create(team);
        }

        private Core.Team Create(Core.Team team)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var teamInfo = mapper.Map<EFContext.Team>(team);
                context.Teams.Add(teamInfo);
                context.SaveChanges();
                return MapTeamFromDB(teamInfo); ;
            }
        }

        private Core.Team Update(Core.Team team)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var teaminfo = context.Teams.Where(t => t.ClubId == team.ClubId && t.TeamId == team.TeamId).FirstOrDefault();
                if (teaminfo == null)
                    return team;

                teaminfo.Name = team.Name;
                context.Entry(teaminfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return MapTeamFromDB(teaminfo);
            }
        }

        private Core.Team MapTeamFromDB(EFContext.Team teamInfo)
        {
            return new Core.Team(teamInfo.ClubId)
            {
                TeamId = teamInfo.TeamId,
                Name = teamInfo.Name
            };
        }
    }
}