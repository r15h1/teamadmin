using System;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

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

            [Fact]
            public void ValuesArePersistedOnCreate()
            {
                Team team = CreateTeamWithNoId();
                Team savedTeam = repo.Save(team);
                Assert.Equal(team.Name, savedTeam.Name);
                Assert.Equal(team.ClubId, savedTeam.ClubId);
            }

            private Team CreateTeamWithNoId()
            {
                return new Team (1){
                    TeamId = null,
                    Name = "Team Name"
                };
            }
        }
    }
}
