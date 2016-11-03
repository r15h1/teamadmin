using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;

namespace TeamAdmin.Lib.Tests.Repositories
{
    public class TeamRepositoryTests
    {
        public class TeamCreation
        {
            private ITeamRepository repo;

            public TeamCreation()
            {
                repo = new TeamRepository();
            }
        }
    }
}
