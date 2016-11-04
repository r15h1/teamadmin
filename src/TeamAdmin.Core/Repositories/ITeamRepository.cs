using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface ITeamRepository
    {
        Team Save(Team team);
        IEnumerable<Team> Get();
        Team Get(int clubId, int teamId);
        bool Delete(int clubId, int teamId);
    }
}
