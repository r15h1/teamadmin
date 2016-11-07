using System;
using System.Collections.Generic;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests.Repositories
{
    /// <summary>
    /// use this for all club and team related operations except for direct club and team crud operations
    /// </summary>
    public class ClubFixture : IDisposable
    {
        private List<Club> clubs = new List<Club> {
            new Club
            {
                Name = "Toronto Tornadoes",
                Address = new Address
                {
                    City = "Toronto",
                    Country = "CA",
                    PostalCode = "M9R1S5",
                    Province = "ON",
                    Street = "333 Dixon Road"
                }
            },

            new Club
            {
                Name = "Caledon Cats",
                Address = new Address
                {
                    City = "Caledon",
                    Country = "CA",
                    PostalCode = "L7C2J3",
                    Province = "ON",
                    Street = "12506 Heart Lake Rd"
                }
            }
        };

        public List<Club> Clubs { get; set; }
        public List<Team> Teams { get; set; }

        public ClubFixture()
        {
            Clubs = new List<Club>();
            Teams = new List<Team>();
            CreateClubFixture();
        }

        private void CreateClubFixture()
        {
            IClubRepository clubRepo = new ClubRepository();
            ITeamRepository teamRepo = new TeamRepository();

            clubs.ForEach((c) => {
                var club = clubRepo.Save(c);
                Clubs.Add(c);
                Teams.Add(teamRepo.Save(new Team(club.ClubId.Value) { Name = "U10" }));
                Teams.Add(teamRepo.Save(new Team(club.ClubId.Value) { Name = "U11" }));
                Teams.Add(teamRepo.Save(new Team(club.ClubId.Value) { Name = "U12" }));
            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }


    [CollectionDefinition("ClubFixtureCollection")]
    public class ClubFixtureCollection : ICollectionFixture<ClubFixture> { }
}
