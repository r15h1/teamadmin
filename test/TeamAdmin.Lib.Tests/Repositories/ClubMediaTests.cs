using System;
using System.Collections.Generic;
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
        public void VerifyThatClubFixureIsNotNull()
        {
            Assert.NotNull(club);
            Assert.NotNull(club.ClubId);
        }

        [Fact]
        public void CanAddMediaToClubs()
        {
            List<Media> mediaList = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image1", Position = 1 },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image2", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video1", Position = 3 }
            };
            IMediaRepository<Club> mediaRepo = new ClubRepository();
            var savedList = mediaRepo.Add(club, mediaList);
            Assert.True(savedList.Count() == mediaList.Count());
        }
    }
}