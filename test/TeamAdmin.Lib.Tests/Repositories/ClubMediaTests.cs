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
        private IMediaRepository<Club> mediaRepo;

        public ClubMediaTests(ClubFixture fixture)
        {
            this.fixture = fixture;
            club = fixture.Clubs.FirstOrDefault();
            mediaRepo = new ClubRepository();
        }

        [Fact]
        public void VerifyThatClubFixureIsNotNull()
        {
            Assert.NotNull(club);
            Assert.NotNull(club.ClubId);
        }

        [Fact]
        public void ValuesArePresistedOnAdd()
        {
            List<Media> mediaList = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image1", Position = 1, Caption = "first image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image2", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video1", Position = 3 }
            };
            var savedList = mediaRepo.AddMedia(club.ClubId.Value, mediaList);
            Assert.True(savedList.Count() == mediaList.Count());
            mediaList.ForEach((c) => {
                var m = savedList.FirstOrDefault(x => x.Caption == c.Caption
                            && x.MediaType == c.MediaType 
                            && x.Position == c.Position 
                            && x.Url.Equals(c.Url));
                Assert.NotNull(m);
            });
        }
        
        [Fact]
        public void MediaCanBeAddedRepeatedly()
        {
            Assert.True(1 == 2);
        }

        [Fact]
        public void AddingMediaRepeatedlyResetsPosition()
        {
            Assert.True(1 == 2);
        }

        [Fact]
        public void MediaCanBeRetrivedAfterAddition()
        {
            Assert.True(1 == 2);
        }
    }
}