using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface ITeamRepository
    {
        Team Save(Team team);
        IEnumerable<Team> Get();
    }
}
