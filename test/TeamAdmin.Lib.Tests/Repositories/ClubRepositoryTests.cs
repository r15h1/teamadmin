using AutoMapper;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests.Repositories
{
    public class ClubRepositoryTests
    {        
        public class ClubCreation{

            [Fact]
            public void ValuesArePersisted()
            {
                Club club = CreateNewClubWithNoId();
                var savedClub = new ClubRepository().Save(club);
                
                Assert.NotNull(savedClub);
                Assert.Equal(club.Name, savedClub.Name);
                Assert.Equal(club.Address.City, savedClub.Address.City);
                Assert.Equal(club.Address.Country, savedClub.Address.Country);
                Assert.Equal(club.Address.PostalCode, savedClub.Address.PostalCode);
                Assert.Equal(club.Address.Province, savedClub.Address.Province);
                Assert.Equal(club.Address.Street, savedClub.Address.Street);
            }

            [Fact]
            public void NewIdIsObtained()
            {
                Club club = CreateNewClubWithNoId();
                var savedClub = new ClubRepository().Save(club);

                Assert.NotNull(savedClub.Id.HasValue && savedClub.Id.Value > 0);                
            }

            [Fact]
            public void CountIncreasesByOne()
            {
                var clubRepository = new ClubRepository();
                int beforeCount = clubRepository.Count;
                Club club = CreateNewClubWithNoId();
                clubRepository.Save(club);
                int afterCount = clubRepository.Count;
                Assert.True(1 == afterCount - beforeCount);
            }

            private static Club CreateNewClubWithNoId()
            {
                return new Club()
                {
                    Id = null,
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
                return new ClubRepository().Save(club);
            }
        }

        public class ClubModification
        {

            [Fact]
            public void ValuesArePersisted()
            {
                IClubRepository repo = new ClubRepository();

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
            public void IdDoesNotChange()
            {
                IClubRepository repo = new ClubRepository();

                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);

                Assert.True(newClub.Id == updatedClub.Id);
            }

            [Fact]
            public void CountRemainsTheSameAfterSave()
            {
                IClubRepository repo = new ClubRepository();                
                Club club = CreateNewClubWithNoId();
                var newClub = repo.Save(club);
                int beforeCount = repo.Count;
                ModifyClubValues(newClub);
                var updatedClub = repo.Save(newClub);
                int afterCount = repo.Count;
                Assert.True(afterCount == beforeCount);
            }

            private Club CreateNewClubWithNoId()
            {
                return new Club()
                {
                    Id = null,
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
                return new ClubRepository().Save(club);
            }
        }

    }
}