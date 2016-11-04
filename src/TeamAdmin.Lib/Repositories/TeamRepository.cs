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
        private object c;

        public TeamRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public IEnumerable<Core.Team> Get()
        {
            var list = new List<Core.Team>();
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var teams = context.Teams.Where( t => !t.Deleted.HasValue || !t.Deleted.Value).ToList();
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
                var teaminfo = context.Teams.FirstOrDefault(t => t.ClubId == team.ClubId && t.TeamId == team.TeamId);
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

        public bool Delete(int clubId, int teamId)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var teamInfo = context.Teams.FirstOrDefault(t => t.ClubId == clubId && t.TeamId == teamId);
                if (teamInfo == null) return false;

                teamInfo.Deleted = true;
                context.Entry(teamInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }

        public Core.Team Get(int clubId, int teamId)
        {
            using (var context = ClubContextFactory.Create<ClubContext>())
            {
                var team = context.Teams.FirstOrDefault(t => t.ClubId == clubId && t.TeamId == teamId && (!t.Deleted.HasValue || !t.Deleted.Value));
                if (team != null)
                        return MapTeamFromDB(team);
            }

            return null;
        }
    }
}