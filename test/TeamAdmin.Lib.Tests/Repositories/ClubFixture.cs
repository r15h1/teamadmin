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
            Config.Init();
            Clubs = new List<Club>();
            Teams = new List<Team>();
            CreateClubFixture();
        }

        private void CreateClubFixture()
        {
            IClubRepository clubRepo = new ClubRepository();
            ITeamRepository teamRepo = new TeamRepository();

            clubs.ForEach((c) => {
                var club = clubRepo.SaveClub(c);
                Clubs.Add(club);
                Teams.Add(teamRepo.SaveTeam(new Team(club.ClubId.Value) { Name = "U10" }));
                Teams.Add(teamRepo.SaveTeam(new Team(club.ClubId.Value) { Name = "U11" }));
                Teams.Add(teamRepo.SaveTeam(new Team(club.ClubId.Value) { Name = "U12" }));
            });
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }


    [CollectionDefinition("ClubFixtureCollection")]
    public class ClubFixtureCollection : ICollectionFixture<ClubFixture> { }

    [CollectionDefinition("ClubMediaFixtureCollection")]
    public class ClubMediaFixtureCollection : ICollectionFixture<ClubFixture> { }

    [CollectionDefinition("TeamFixtureCollection")]
    public class TeamFixtureCollection : ICollectionFixture<ClubFixture> { }

    [CollectionDefinition("TeamMediaFixtureCollection")]
    public class TeamMediaFixtureCollection : ICollectionFixture<ClubFixture> { }

    [CollectionDefinition("EventFixtureCollection")]
    public class EventFixtureCollection : ICollectionFixture<ClubFixture> { }

    [CollectionDefinition("PostFixtureCollection")]
    public class PostFixtureCollection : ICollectionFixture<ClubFixture> { }
}
