using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface ITeamRepository
    {
        Team SaveTeam(Team team);
        IEnumerable<Team> GetTeams(string name = null);
        Team GetTeam(int teamId);
        bool DeleteTeam(int teamId);
    }
}
