using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests.Repositories
{
    [Collection("TeamFixtureCollection")]
    public class TeamMediaTests
    {
        private ClubFixture fixture;
        private Club club;
        private IMediaRepository<Team> mediaRepo;
        private Team team;

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

        public TeamMediaTests(ClubFixture fixture)
        {
            this.fixture = fixture;
            club = fixture.Clubs.FirstOrDefault();
            team = fixture.Teams.FirstOrDefault();
            mediaRepo = new TeamRepository();
        }

        [Fact]
        public void VerifyThatTeamIsNotNull()
        {
            Assert.NotNull(team);
            Assert.NotNull(team.TeamId);
        }

        [Fact]
        public void ValuesArePresistedOnAdd()
        {
            var beforeCount = mediaRepo.GetMediaCount(team.TeamId.Value);
            var savedList = mediaRepo.AddMedia(team.TeamId.Value, mediaList1);
            var afterCount = mediaRepo.GetMediaCount(team.TeamId.Value);

            Assert.True(savedList.Count() == mediaList1.Count());
            Assert.True(savedList.Count(x => x.MediaId.HasValue) == mediaList1.Count());
            Assert.True(afterCount == beforeCount + savedList.Count());
        }
        
        [Fact]
        public void AddingMediaRepeatedlyResetsPosition()
        {
            var beforeCount = mediaRepo.GetMediaCount(team.TeamId.Value);
            var savedList1 = mediaRepo.AddMedia(team.TeamId.Value, mediaList1);
            var savedList2 = mediaRepo.AddMedia(team.TeamId.Value, mediaList2);

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
            var newId = mediaRepo.AddMedia(team.TeamId.Value, new List<Media> { media }).FirstOrDefault().MediaId;
            var retrievedMedia = mediaRepo.GetMedia(team.TeamId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.NotNull(retrievedMedia);
        }

        [Fact]
        public void CountIsEqual()
        {
            Assert.True(mediaRepo.GetMediaCount(team.TeamId.Value) == mediaRepo.GetMedia(team.TeamId.Value).Count());
        }

        [Fact]
        public void DeletedMediaCannotBeRetrieved()
        {
            var savedList1 = mediaRepo.AddMedia(team.TeamId.Value, mediaList1);
            var media = savedList1.FirstOrDefault(m => m.MediaId == savedList1.FirstOrDefault().MediaId);
            Assert.NotNull(media);
            bool success = mediaRepo.DeleteMedia(media.MediaId.Value);
            Assert.True(success);
            Assert.Null(mediaRepo.GetMedia(team.TeamId.Value).FirstOrDefault(m => m.MediaId == media.MediaId));
        }

        [Fact]
        public void DeletingMediaReducesCount()
        {
            var savedList1 = mediaRepo.AddMedia(team.TeamId.Value, mediaList1);
            var media = savedList1.FirstOrDefault(m => m.MediaId == savedList1.FirstOrDefault().MediaId);
            Assert.NotNull(media);
            var beforeCount = mediaRepo.GetMediaCount(team.TeamId.Value);
            bool success = mediaRepo.DeleteMedia(media.MediaId.Value);
            Assert.True(success);
            var afterCount = mediaRepo.GetMediaCount(team.TeamId.Value);
            Assert.True(afterCount == beforeCount - 1);            
        }

        [Fact]
        public void MediaCaptionIsUpdated()
        {
            string updatedCaption = "Updated the awesome caption";
            var originalMedia = new Media { MediaType = MediaType.IMAGE, Url = "http://www.images.com/myimage001.jpg", Position = 1, Caption = "awesome image" };
            var newId = mediaRepo.AddMedia(team.TeamId.Value, new List<Media> { originalMedia }).FirstOrDefault().MediaId;
            var retrievedMedia = mediaRepo.GetMedia(team.TeamId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.True(retrievedMedia.Caption.Equals(originalMedia.Caption));
            mediaRepo.UpdateMediaCaption(retrievedMedia.MediaId.Value, updatedCaption);
            retrievedMedia = mediaRepo.GetMedia(team.TeamId.Value).FirstOrDefault(m => m.MediaId == newId);
            Assert.True(retrievedMedia.Caption.Equals(updatedCaption));
        }
    }
}