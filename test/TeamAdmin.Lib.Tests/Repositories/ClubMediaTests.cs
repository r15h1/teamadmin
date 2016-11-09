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

        private List<Media> mediaList1 = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image1", Position = 1, Caption = "first image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image2", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video1", Position = 3 }
            };

        private List<Media> mediaList2 = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image3", Position = 1, Caption = "third image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image4", Position = 2 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video5", Position = 3 , Caption = "1st video" },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video6", Position = 4 }
            };

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
            var beforeCount = mediaRepo.GetMediaCount(club.ClubId.Value);
            var savedList = mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
            var afterCount = mediaRepo.GetMediaCount(club.ClubId.Value);

            Assert.True(savedList.Count() == mediaList1.Count());
            Assert.True(savedList.Count(x => x.MediaId.HasValue) == mediaList1.Count());
            Assert.True(afterCount == beforeCount + savedList.Count());
        }
        
        [Fact]
        public void AddingMediaRepeatedlyResetsPosition()
        {
            var beforeCount = mediaRepo.GetMediaCount(club.ClubId.Value);
            var savedList1 = mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
            var savedList2 = mediaRepo.AddMedia(club.ClubId.Value, mediaList2);

            Assert.True(savedList1.Count() == mediaList1.Count());
            Assert.True(savedList2.Count() == mediaList2.Count());

            mediaList2.ForEach((c) => {
                var m = savedList2.FirstOrDefault(x => x.Caption == c.Caption
                            && x.MediaType == c.MediaType
                            && x.Url.Equals(c.Url));
                Assert.NotNull(m);
                Assert.True(m.Position >= c.Position + mediaList1.Count + beforeCount);
            });
        }


        [Fact]
        public void MediaCanBeRetrivedAfterAddition()
        {
            var media = new Media { MediaType = MediaType.IMAGE, Url = "http://www.images.com/myimage001.jpg", Position = 1, Caption = "awesome image" };
            var newId = mediaRepo.AddMedia(club.ClubId.Value, new List<Media> { media }).FirstOrDefault().MediaId;
            var retrievedMedia = mediaRepo.GetMedia(club.ClubId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.NotNull(retrievedMedia);
        }

        [Fact]
        public void CountIsEqual()
        {
            Assert.True(mediaRepo.GetMediaCount(club.ClubId.Value) == mediaRepo.GetMedia(club.ClubId.Value).Count());
        }

        [Fact]
        public void DeletedMediaCannotBeRetrieved()
        {
            var savedList1 = mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
            var media = savedList1.FirstOrDefault(m => m.MediaId == savedList1.FirstOrDefault().MediaId);
            Assert.NotNull(media);
            bool success = mediaRepo.DeleteMedia(media.MediaId.Value);
            Assert.True(success);
            Assert.Null(mediaRepo.GetMedia(club.ClubId.Value).FirstOrDefault(m => m.MediaId == media.MediaId));
        }

        [Fact]
        public void DeletingMediaReducesCount()
        {
            var savedList1 = mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
            var media = savedList1.FirstOrDefault(m => m.MediaId == savedList1.FirstOrDefault().MediaId);
            Assert.NotNull(media);
            var beforeCount = mediaRepo.GetMediaCount(club.ClubId.Value);
            bool success = mediaRepo.DeleteMedia(media.MediaId.Value);
            Assert.True(success);
            var afterCount = mediaRepo.GetMediaCount(club.ClubId.Value);
            Assert.True(afterCount == beforeCount - 1);            
        }

        [Fact]
        public void MediaCaptionIsUpdated()
        {
            string updatedCaption = "Updated the awesome caption";
            var originalMedia = new Media { MediaType = MediaType.IMAGE, Url = "http://www.images.com/myimage001.jpg", Position = 1, Caption = "awesome image" };
            var newId = mediaRepo.AddMedia(club.ClubId.Value, new List<Media> { originalMedia }).FirstOrDefault().MediaId;
            var retrievedMedia = mediaRepo.GetMedia(club.ClubId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.True(retrievedMedia.Caption.Equals(originalMedia.Caption));
            mediaRepo.UpdateMediaCaption(retrievedMedia.MediaId.Value, updatedCaption);
            retrievedMedia = mediaRepo.GetMedia(club.ClubId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.True(retrievedMedia.Caption.Equals(updatedCaption));
        }
    }
}