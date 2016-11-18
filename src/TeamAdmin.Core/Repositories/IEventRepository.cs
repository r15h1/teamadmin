using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IEventRepository
    {
        Event CreateEvent(Club club, Event evnt);
        Event CreateEvent(IEnumerable<Team> teams, Event evnt);
        bool DeleteEvent(long eventId);
        Event GetEvent(long value);
        IEnumerable<Event> GetEvents(Team team);
        IEnumerable<Event> GetEvents(Club club);
    }
}