using System;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests.Repositories
{
    [Collection("ClubFixtureCollection")]
    public class ClubMediaTests
    {
        private ClubFixture fixture;
        private Club club;

        public ClubMediaTests(ClubFixture fixture)
        {
            this.fixture = fixture;
            club = fixture.Clubs.FirstOrDefault();
        }

        [Fact]
        public void MediaListIsPersistedToDB()
        {
            
        }        
    }
}
