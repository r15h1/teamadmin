﻿using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace TeamAdmin.Lib.Tests.Repositories
{
    public class ClubRepositoryTests
    {        
        public class ClubCreation{

            private IClubRepository repo;

            public ClubCreation()
            {
                repo = new ClubRepository();
            }

            [Fact]
            public void ValuesArePersistedOnCreate()
            {
                Club club = CreateNewClubWithNoId();
                var savedClub = repo.Save(club);
                
                Assert.NotNull(savedClub);
                Assert.Equal(club.Name, savedClub.Name);
                Assert.Equal(club.Address.City, savedClub.Address.City);
                Assert.Equal(club.Address.Country, savedClub.Address.Country);
                Assert.Equal(club.Address.PostalCode, savedClub.Address.PostalCode);
                Assert.Equal(club.Address.Province, savedClub.Address.Province);
                Assert.Equal(club.Address.Street, savedClub.Address.Street);
            }

            [Fact]
            public void CanRetrieveClubByIdUponCreation()
            {
                var club = CreateNewClubWithNoId();
                var savedClub = repo.Save(club);
                var retrievedClub = repo.Get(savedClub.ClubId.Value);
                Assert.Equal(retrievedClub.ClubId, savedClub.ClubId);
                Assert.Equal(retrievedClub.Name, savedClub.Name);
                Assert.Equal(retrievedClub.Address.City, savedClub.Address.City);
                Assert.Equal(retrievedClub.Address.Country, savedClub.Address.Country);
                Assert.Equal(retrievedClub.Address.PostalCode, savedClub.Address.PostalCode);
                Assert.Equal(retrievedClub.Address.Province, savedClub.Address.Province);
                Assert.Equal(retrievedClub.Address.Street, savedClub.Address.Street);
            }

            [Fact]
            public void NewIdIsObtainedOnCreate()
            {
                Club club = CreateNewClubWithNoId();
                var savedClub = repo.Save(club);

                Assert.NotNull(savedClub.ClubId.HasValue && savedClub.ClubId.Value > 0);                
            }

            [Fact]
            public void CountIncreasesByOneOnCreate()
            {
                var listBefore = repo.Get();
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                var listAfter = repo.Get();
                Assert.True(listBefore.Count(c => c.ClubId.Value == newClub.ClubId) == 0);
                Assert.True(listAfter.Count(c => c.ClubId.Value == newClub.ClubId) == 1);
            }

            private static Club CreateNewClubWithNoId()
            {
                return new Club()
                {
                    ClubId = null,
                    Name = "Test Club",
                    Address = new Address
                    {
                        Street = "Test Address",
                        City = "Test City",
                        Province = "Test Province",
                        Country = "Test Country",
                        PostalCode = "TestPC"
                    }
                };
            }

            private Club SaveClub(Club club)
            {
                return repo.Save(club);
            }
        }

        public class ClubModification
        {
            private IClubRepository repo;

            public ClubModification()
            {
                repo = new ClubRepository();
            }

            [Fact]
            public void ValuesArePersistedOnUpdate()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);

                Assert.NotNull(updatedClub);
                Assert.Equal(updatedClub.Name, newClub.Name);
                Assert.Equal(updatedClub.Address.City, newClub.Address.City);
                Assert.Equal(updatedClub.Address.Country, newClub.Address.Country);
                Assert.Equal(updatedClub.Address.PostalCode, newClub.Address.PostalCode);
                Assert.Equal(updatedClub.Address.Province, newClub.Address.Province);
                Assert.Equal(updatedClub.Address.Street, newClub.Address.Street);
            }

            private static void ModifyClubValues(Club newClub)
            {
                newClub.Address.City = "Toronto";
                newClub.Address.Country = "US";
                newClub.Address.PostalCode = "123456";
                newClub.Address.Province = "BC";
                newClub.Address.Street = "A Street Somewhere";
            }

            [Fact]
            public void IdDoesNotChangeOnUpdate()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);
                Assert.True(newClub.ClubId == updatedClub.ClubId);
            }

            [Fact]
            public void CanRetrieveClubByIdUponModification()
            {
                var club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);
                var retrievedClub = repo.Get(updatedClub.ClubId.Value);
                Assert.Equal(retrievedClub.ClubId, updatedClub.ClubId);
                Assert.Equal(retrievedClub.Name, updatedClub.Name);
                Assert.Equal(retrievedClub.Address.City, updatedClub.Address.City);
                Assert.Equal(retrievedClub.Address.Country, updatedClub.Address.Country);
                Assert.Equal(retrievedClub.Address.PostalCode, updatedClub.Address.PostalCode);
                Assert.Equal(retrievedClub.Address.Province, updatedClub.Address.Province);
                Assert.Equal(retrievedClub.Address.Street, updatedClub.Address.Street);
            }

            [Fact]
            public void CountDoesNotChangeOnUpdate()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                var listBefore = repo.Get();
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);
                var listAfter = repo.Get();
                Assert.True(listBefore.Count(c => c.ClubId.Value == newClub.ClubId) == 1);
                Assert.True(listAfter.Count(c => c.ClubId.Value == newClub.ClubId) == 1);
            }

            private Club CreateNewClubWithNoId()
            {
                return new Club()
                {
                    ClubId = null,
                    Name = "Test Club",
                    Address = new Address
                    {
                        Street = "Test Address",
                        City = "Test City",
                        Province = "Test Province",
                        Country = "Test Country",
                        PostalCode = "TestPC"
                    }
                };
            }

            private Club SaveClub(Club club)
            {
                return repo.Save(club);
            }
        }

        public class ClubDeletion
        {
            private IClubRepository repo;

            public ClubDeletion()
            {
                repo = new ClubRepository();
            }

            [Fact]
            public void ExistingClubDeletedWithSuccess()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                bool result = repo.Delete(newClub.ClubId.Value);
                Assert.True(result);
            }

            [Fact]
            public void CountReducesByOneOnDelete()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                var listBefore = repo.Get();
                repo.Delete(newClub.ClubId.Value);
                var listAfter = repo.Get(); 
                Assert.True(listBefore.Count(c => c.ClubId.Value == newClub.ClubId) == 1);
                Assert.True(listAfter.Count(c => c.ClubId.Value == newClub.ClubId) == 0);
            }

            [Fact]
            public void CanNotRetrieveClubByIdUponDeletion()
            {
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                var retrievedClubBeforeDel = repo.Get(newClub.ClubId.Value);
                repo.Delete(newClub.ClubId.Value);
                var retrievedClubAfterDel = repo.Get(newClub.ClubId.Value);
                Assert.Equal(retrievedClubBeforeDel.ClubId, newClub.ClubId);
                Assert.NotNull(retrievedClubBeforeDel);
                Assert.Null(retrievedClubAfterDel);
            }

            private Club CreateNewClubWithNoId()
            {
                return new Club()
                {
                    ClubId = null,
                    Name = "Test Club",
                    Address = new Address
                    {
                        Street = "Test Address",
                        City = "Test City",
                        Province = "Test Province",
                        Country = "Test Country",
                        PostalCode = "TestPC"
                    }
                };
            }
        }
    }
}