using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface ITeamRepository
    {
        Team SaveTeam(Team team);
        IEnumerable<Team> GetTeams();
        Team GetTeam(int clubId, int teamId);
        bool DeleteTeam(int clubId, int teamId);
    }
}
