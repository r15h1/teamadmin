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
        public void AddingMediaRepeatedlyResetsPosition()
        {
            var existingMediaCount = mediaRepo.GetMediaCount(club.ClubId.Value);

            List<Media> mediaList1 = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image1", Position = 1, Caption = "first image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image2", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video1", Position = 3 }
            };
            List<Media> mediaList2 = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image3", Position = 1, Caption = "third image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image4", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video5", Position = 3 , Caption = "1st video" },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video6", Position = 4 }
            };
            var savedList1 = mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
            var savedList2 = mediaRepo.AddMedia(club.ClubId.Value, mediaList2);

            Assert.True(savedList1.Count() == mediaList1.Count());
            Assert.True(savedList2.Count() == mediaList2.Count());

            mediaList2.ForEach((c) => {
                var m = savedList2.FirstOrDefault(x => x.Caption == c.Caption
                            && x.MediaType == c.MediaType
                            && x.Url.Equals(c.Url));
                Assert.NotNull(m);
                Assert.True(m.Position == c.Position + mediaList1.Count + existingMediaCount);
            });
        }


        //[Fact]
        //public void MediaCanBeRetrivedAfterAddition()
        //{
        //    Assert.True(1 == 2);
        //}
    }
}