using AutoMapper;
using System;
using System.Collections.Generic;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;

namespace TeamAdmin.Lib.Repositories
{
    public class EventRepository : IEventRepository
    {
        IMapper mapper;

        public EventRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public void CreateEvent(IEnumerable<Core.Team> teams, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            foreach (var team in teams) ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = team.ClubId, TeamId = team.TeamId });
            SaveEvent(ev);
        }

        public void CreateEvent(Core.Club club, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value });
            SaveEvent(ev);
        }

        private void SaveEvent(EFContext.Event evnt)
        {
            using (var context = ContextFactory.Create<EventContext>())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Events.Add(evnt);
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}