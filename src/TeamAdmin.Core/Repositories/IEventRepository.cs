using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IEventRepository
    {
        void CreateEvent(Club club, Event evnt);
        void CreateEvent(IEnumerable<Team> teams, Event evnt);
    }
}