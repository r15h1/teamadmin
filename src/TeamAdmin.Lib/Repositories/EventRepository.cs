using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Core.Event SaveEvent(IEnumerable<Core.Team> teams, Core.Event evnt)
        {
            var ev = mapper.Map<EFContext.Event>(evnt);
            foreach (var team in teams) ev.ClubTeamEvents.Add(new ClubTeamEvent { ClubId = team.ClubId, TeamId = team.TeamId });
            //return SaveEvent(ev);
            return null;
        }

        public Core.Event SaveEvent(Core.Club club, Core.Event evnt)
        {
            if (evnt.EventType != EventType.GAME) evnt.Competition = null;

            if (evnt.EventType != EventType.GAME && evnt.EventType != EventType.EXHIBITION_GAME) {
                evnt.Opponent = null;
                evnt.Away = null;
            }

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
                    eventItem.Address = evnt.Address;
                    eventItem.OpponentId = evnt.Opponent != null ? evnt.Opponent.OpponentId : null;
                    eventItem.CompetitionId = evnt.Competition != null ? evnt.Competition.CompetitionId : null;
                    eventItem.Away = evnt.Away;

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
                var ev = context.ClubTeamEvents.Include(c => c.Event).ThenInclude(e => e.Opponent)
                        .Include(c => c.Team)
                        .Where(c => c.EventId == eventId);

                var efevent = ev.Select(c => c.Event).ToList().FirstOrDefault();

                var evnt = mapper.Map<Core.Event>(efevent);
                evnt.Teams = mapper.Map<List<Core.Team>>(ev.Select(c => c.Team).ToList());
                evnt.Opponent = mapper.Map<Core.Opponent>(ev.Select(e => e.Event.Opponent).FirstOrDefault());
                evnt.Competition = mapper.Map<Core.Competition>(ev.Select(e => e.Event.Competition).FirstOrDefault());
                evnt.GameResult = string.IsNullOrWhiteSpace(efevent.Result) ? null : new GameResult(efevent.Result);
                return evnt;
            }
        }

        public IEnumerable<Core.Event> GetEvents(Core.Club club)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                return context.ClubTeamEvents.Include(c => c.Event).ThenInclude(e => e.Opponent).Include(c => c.Team)
                        .Where(c => c.ClubId == club.ClubId.Value && c.Event.EndDate >= DateTime.Today)
                        .GroupBy(x => x.Event, (e, cte) => new Core.Event
                        {
                            Description = e.Description,
                            EndDate = e.EndDate,
                            EventId = e.EventId,
                            EventType = (EventType)e.EventType,
                            StartDate = e.StartDate,
                            Title = e.Title,
                            Address = e.Address,
                            Teams = cte.Select(x => new Core.Team(club.ClubId.Value) { Name = x.Team.Name, DisplayName = x.Team.DisplayName }).ToList(),
                            Opponent = mapper.Map<Core.Opponent>(cte.Select(t=> t.Event.Opponent).FirstOrDefault()),
                            Competition = mapper.Map<Core.Competition>(cte.Select(t => t.Event.Competition).FirstOrDefault()),
                            Away = e.Away,
                            GameResult = string.IsNullOrWhiteSpace(e.Result) ? null : new GameResult(e.Result)
                        }).OrderBy(e => e.StartDate).ThenBy(e => e.EndDate).ToList();
            }            
        }

        public IEnumerable<Core.Event> GetEvents(Core.Team team)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                return context.ClubTeamEvents.Include(c => c.Event).ThenInclude(e => e.Opponent).Include(c => c.Team)
                        .Where(c => c.TeamId == team.TeamId.Value && c.Event.EndDate >= DateTime.Today)
                        .GroupBy(x => x.Event, (e, cte) => new Core.Event
                        {
                            Description = e.Description,
                            EndDate = e.EndDate,
                            EventId = e.EventId,
                            EventType = (EventType)e.EventType,
                            StartDate = e.StartDate,
                            Title = e.Title,
                            Address = e.Address,
                            Teams = cte.Select(x => new Core.Team(team.ClubId) { Name = x.Team.Name, DisplayName = x.Team.DisplayName }).ToList(),
                            Opponent = mapper.Map<Core.Opponent>(cte.Select(t => t.Event.Opponent).FirstOrDefault()),
                            Competition = mapper.Map<Core.Competition>(cte.Select(t => t.Event.Competition).FirstOrDefault()),
                            Away = e.Away,
                            GameResult = string.IsNullOrWhiteSpace(e.Result) ? null : new GameResult(e.Result)
                        }).OrderBy(e => e.StartDate).ThenBy(e => e.EndDate).ToList();
            }        
        }

        public void UpdateResult(long eventId, GameResult result)
        {
            using (var context = ContextFactory.Create<EventContext>())
            {
                var ev = context.Events.Where(c => c.EventId == eventId).FirstOrDefault();
                ev.Result = $"{result.Team1Score}|{result.Team2Score}";
                context.SaveChanges();
            }
        }
    }
}