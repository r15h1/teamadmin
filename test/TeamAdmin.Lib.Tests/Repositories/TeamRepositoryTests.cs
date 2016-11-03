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
                Team newTeam = repo.Save(team);
                Assert.Equal(team.Name, newTeam.Name);
                Assert.Equal(team.ClubId, newTeam.ClubId);
            }

            [Fact]
            public void NewIdIsObtainedOnCreate()
            {
                Team team = CreateTeamWithNoId();
                Team newTeam = repo.Save(team);
                Assert.True(newTeam.TeamId.HasValue && newTeam.TeamId.Value > 0);
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
