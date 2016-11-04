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
                var team = CreateTeamWithNoId();
                Team newTeam = repo.Save(team);
                Assert.Equal(team.Name, newTeam.Name);
                Assert.Equal(team.ClubId, newTeam.ClubId);
            }

            [Fact]
            public void NewIdIsObtainedOnCreate()
            {
                var team = CreateTeamWithNoId();
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

            [Fact]
            public void CanRetrieveTeamByIdUponCreation()
            {
                var team = CreateTeamWithNoId();
                var newTeam = repo.Save(team);
                var retrievedTeam = repo.Get(newTeam.ClubId, newTeam.TeamId.Value);
                Assert.NotNull(retrievedTeam);
                Assert.True(retrievedTeam.ClubId == newTeam.ClubId);
                Assert.True(retrievedTeam.TeamId == newTeam.TeamId);
                Assert.True(retrievedTeam.Name == newTeam.Name);
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
                var team = CreateNewTeamWithNoId();
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

            [Fact]
            public void CanRetrieveTeamByIdUponModification()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                ModifyTeamValues(newTeam);
                var updatedTeam = repo.Save(newTeam);
                var retrievedTeam = repo.Get(updatedTeam.ClubId, updatedTeam.TeamId.Value);
                Assert.NotNull(retrievedTeam);
                Assert.True(retrievedTeam.ClubId == updatedTeam.ClubId);
                Assert.True(retrievedTeam.TeamId == updatedTeam.TeamId);
                Assert.True(retrievedTeam.Name == updatedTeam.Name);
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

        public class TeamDeletion
        {
            private ITeamRepository repo;

            public TeamDeletion()
            {
                repo = new TeamRepository();
            }

            [Fact]
            public void ExistingTeamDeletedWithSuccess()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                bool result = repo.Delete(newTeam.ClubId, newTeam.TeamId.Value);
                Assert.True(result);
            }

            [Fact]
            public void CountReducesByOneOnDelete()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                var listBefore = repo.Get();
                repo.Delete(newTeam.ClubId, newTeam.TeamId.Value);
                var listAfter = repo.Get();
                Assert.True(listBefore.Where(t => t.ClubId == newTeam.ClubId && t.TeamId == newTeam.TeamId).Count() == 1);
                Assert.True(listAfter.Where(t => t.ClubId == newTeam.ClubId && t.TeamId == newTeam.TeamId).Count() == 0);
            }

            [Fact]
            public void CanNotRetrieveClubByIdUponDeletion()
            {
                var team = CreateNewTeamWithNoId();
                var newTeam = repo.Save(team);
                var beforeDelete = repo.Get(newTeam.ClubId, newTeam.TeamId.Value);
                repo.Delete(newTeam.ClubId, newTeam.TeamId.Value);
                var afterDelete = repo.Get(newTeam.ClubId, newTeam.TeamId.Value);
                Assert.NotNull(beforeDelete);
                Assert.Equal(beforeDelete.ClubId, newTeam.ClubId);
                Assert.Equal(beforeDelete.TeamId, newTeam.TeamId);
                Assert.Null(afterDelete);
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
