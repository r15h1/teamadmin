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

        public IEnumerable<Core.Event> GetEvents(Core.Team team)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                return  (from e in context.Events
                            join t in context.ClubTeamEvents on e.EventId equals t.EventId into te
                            from t1 in te
                            where t1.TeamId == team.TeamId
                            select new Core.Event
                            {
                                Description = e.Description,
                                EndDate = e.EndDate,
                                EventId = e.EventId,
                                EventType = (EventType)Enum.Parse(typeof(EventType), e.EventType.ToString()),
                                StartDate = e.StartDate,
                                Title = e.Title
                            }
                            ).ToList();
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