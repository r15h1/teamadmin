using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories
{
    public class EventRepository : IEventRepository
    {
        IMapper mapper;

        public EventRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public Core.Event CreateEvent(IEnumerable<Core.Team> teams, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            foreach (var team in teams) ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = team.ClubId, TeamId = team.TeamId });
            return SaveEvent(ev);
        }

        public Core.Event CreateEvent(Core.Club club, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value });
            return SaveEvent(ev);
        }

        public bool DeleteEvent(long eventId)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                var evnt = context.Events.Include(c => c.ClubTeamEvents).FirstOrDefault(m => m.EventId == eventId);
                if (evnt == null) return false;
                
                context.Events.Remove(evnt);
                context.SaveChanges();
                return true;
            }
        }

        public Core.Event GetEvent(long eventId)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                var evnt = context.Events.FirstOrDefault(m => m.EventId == eventId);
                return mapper.Map<Core.Event>(evnt);
            }
        }

        private Core.Event SaveEvent(EFContext.Event evnt)
        {
            using (var context = ContextFactory.Create<EventContext>())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Events.Add(evnt);
                    context.SaveChanges();
                    transaction.Commit();
                    return mapper.Map<Core.Event>(evnt);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return null;
        }
    }
}