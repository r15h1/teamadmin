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

        public Core.Event SaveEvent(IEnumerable<Core.Team> teams, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            foreach (var team in teams) ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = team.ClubId, TeamId = team.TeamId });
            //return SaveEvent(ev);
            return null;
        }

        public Core.Event SaveEvent(Core.Club club, Core.Event evnt)
        {
            if (evnt.EventId.HasValue)
                return UpdateEvent(club, evnt);

            return CreateEvent(club, evnt);            
        }

        private Core.Event CreateEvent(Core.Club club, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);

            if (evnt.Teams.Count > 0)
            {
                foreach (var t in evnt.Teams)
                    ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value, TeamId = t.TeamId });
            }
            else
            {
                ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value});
            }

            using (var context = ContextFactory.Create<EventContext>())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Events.Add(ev);
                    context.SaveChanges();
                    transaction.Commit();
                    return mapper.Map<Core.Event>(ev);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return null;
        }

        private Core.Event UpdateEvent(Core.Club club, Core.Event evnt)
        {
            using (var context = ContextFactory.Create<EventContext>())
            using (var transaction = context.Database.BeginTransaction())
            {
                var eventItem = context.Events.Include(c => c.ClubTeamEvents).FirstOrDefault(e => e.EventId == evnt.EventId);
                if (eventItem == null) return null;

                try
                {
                    eventItem.Description = evnt.Description;
                    eventItem.EndDate = evnt.EndDate;
                    eventItem.EventType = (byte) evnt.EventType;
                    eventItem.StartDate = evnt.StartDate;
                    eventItem.Title = evnt.Title;
                    
                    if (eventItem.ClubTeamEvents != null && eventItem.ClubTeamEvents.Count > 0)
                        foreach (var cev in eventItem.ClubTeamEvents)
                            context.Remove(cev);

                    if (evnt.Teams != null && evnt.Teams.Count > 0)
                    {
                        foreach (var t in evnt.Teams)
                            eventItem.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value, TeamId = t.TeamId });
                    }
                    else {
                        eventItem.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = club.ClubId.Value });
                    }

                    context.SaveChanges();
                    transaction.Commit();
                    return mapper.Map<Core.Event>(eventItem);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return null;
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
                var evnt = context.Events.Include(c => c.ClubTeamEvents).FirstOrDefault(m => m.EventId == eventId);
                return mapper.Map<Core.Event>(evnt);
            }
        }

        public IEnumerable<Core.Event> GetEvents(Core.Club club)
        {
            //using (var context = ContextFactory.Create<EventContext>())
            //{
            //    return (from e in context.Events 
            //            join cte in context.ClubTeamEvents on e.EventId equals cte.EventId                                                                                         
            //            join tm in context.Teams on cte.TeamId equals tm.TeamId
            //            into t1
            //            where cte.ClubId == club.ClubId 
            //            select new Core.Event
            //            {
            //                Description = e.Description,
            //                EndDate = e.EndDate,
            //                EventId = e.EventId,
            //                EventType = (EventType)Enum.Parse(typeof(EventType), e.EventType.ToString()),
            //                StartDate = e.StartDate,
            //                Title = e.Title,
            //                Teams = t1.Select(t => new Core.Team(club.ClubId.Value)).ToList()
            //            }
            //            ).GroupBy(x => x.EventId).Select(x => x.First()).ToList();
            //}

            using (var context = ContextFactory.Create<EventContext>())
            {
                return context.ClubTeamEvents.Include(c => c.Event).Include(c => c.Team).Where(c => c.ClubId == club.ClubId.Value)
                    .Select(e => new Core.Event {
                        Description = e.Event.Description,
                        EndDate = e.Event.EndDate,
                        EventId = e.EventId,
                        EventType = (EventType)e.Event.EventType,
                        StartDate = e.Event.StartDate,
                        Title = e.Event.Title,
                        Teams = new List<Core.Team> { new Core.Team(club.ClubId.Value) { Name = e.Team.Name, TeamId = e.TeamId }}
                    }).ToList();                    
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
    }
}