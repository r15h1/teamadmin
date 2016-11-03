using System;
using System.Linq;
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

            [Fact]
            public void CountIncreasesByOneOnCreate()
            {
                var listBefore = repo.Get();
                var team = CreateTeamWithNoId();
                var newTeam = repo.Save(team);
                var listAfter = repo.Get();
                Assert.True(listBefore.Where(t => t.TeamId.Value == newTeam.TeamId).Count() == 0);
                Assert.True(listAfter.Where(t => t.TeamId.Value == newTeam.TeamId).Count() == 1);
            }

            private Team CreateTeamWithNoId()
            {
                return new Team (1){
                    TeamId = null,
                    Name = "Team Name"
                };
            }
        }

        public class TeamModification
        {
            private ITeamRepository repo;

            public TeamModification()
            {
                repo = new TeamRepository();
            }

            [Fact]
            public void ValuesArePersistedOnUpdate()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                ModifyTeamValues(newTeam);
                var updatedTeam = repo.Save(newTeam);

                Assert.NotNull(updatedTeam);
                Assert.Equal(updatedTeam.Name, newTeam.Name);
                Assert.Equal(updatedTeam.ClubId, newTeam.ClubId);
            }

            [Fact]
            public void IdDoesNotChangeOnUpdate()
            {
                Team team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                ModifyTeamValues(newTeam);
                var updatedTeam = repo.Save(newTeam);

                Assert.True(newTeam.ClubId == updatedTeam.ClubId);
                Assert.True(newTeam.TeamId == updatedTeam.TeamId);
            }

            [Fact]
            public void CountDoesNotChangeOnUpdate()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                var listBefore = repo.Get();
                ModifyTeamValues(newTeam);
                var updatedClub = repo.Save(newTeam);
                var listAfter = repo.Get();
                Assert.True(listBefore.Where(t => t.ClubId == newTeam.ClubId && t.TeamId.Value == newTeam.TeamId).Count() == 1);
                Assert.True(listAfter.Where(t => t.ClubId == newTeam.ClubId && t.TeamId.Value == newTeam.TeamId).Count() == 1);
            }


            private void ModifyTeamValues(Team team)
            {
                team.Name = "New Name";
            }

            private Team CreateNewTeamWithNoId()
            {
                return new Team(1) 
                {
                    TeamId = null,
                    Name = "Team Name"
                };
            }
        }
    }
}
