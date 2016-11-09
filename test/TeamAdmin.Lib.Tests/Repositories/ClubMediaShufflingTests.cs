using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests.Repositories
{
    [Collection("ClubMediaFixtureCollection")]
    public class ClubMediaShufflingTests
    {
        private ClubFixture fixture;
        private Club club;
        private IMediaRepository<Club> mediaRepo;

        private List<Media> mediaList1 = new List<Media> {
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image1", Position = 1, Caption = "first image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image2", Position = 2 },                
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image3", Position = 3, Caption = "third image" },
                new Media {  MediaType = MediaType.IMAGE, Url = "http://www.images.com/image4", Position = 4 },

                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video1", Position = 1 },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video5", Position = 2 , Caption = "1st video" },
                new Media {  MediaType = MediaType.VIDEO, Url = "http://www.youtube.com/video6", Position = 3 }
            };
        private object allMediaListBefore;

        public ClubMediaShufflingTests(ClubFixture fixture)
        {
            this.fixture = fixture;
            club = fixture.Clubs.FirstOrDefault();
            mediaRepo = new ClubRepository();
            if (mediaRepo.GetMediaCount(club.ClubId.Value) == 0) mediaRepo.AddMedia(club.ClubId.Value, mediaList1);
        }
        
        [Fact]
        public void MediaPositionCanBeIncreasedIndependentOfOtherTypes()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn2Before = imagesBefore.ElementAt(1);
            var result = mediaRepo.SetMediaPosition(imageAtPosn2Before.MediaId.Value, 4);

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.True(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(0).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(3).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(1).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(2).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

        [Fact]
        public void MediaPositionCanBeDecreasedIndependentOfOtherTypes()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn4Before = imagesBefore.ElementAt(3);
            var result = mediaRepo.SetMediaPosition(imageAtPosn4Before.MediaId.Value, 2);

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.True(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(0).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(2).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(3).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(1).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

        [Fact]
        public void MediaPositionCannotBeIncreasedBeyondCount()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn2Before = imagesBefore.ElementAt(1);
            var result = mediaRepo.SetMediaPosition(imageAtPosn2Before.MediaId.Value, imagesBefore.Count() + 1);

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.False(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(0).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(1).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(2).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(3).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

        [Fact]
        public void MediaPositionCannotBeDecreasedBeyondOne()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn4Before = imagesBefore.ElementAt(3);
            var result = mediaRepo.SetMediaPosition(imageAtPosn4Before.MediaId.Value, 0);

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.False(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(0).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(1).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(2).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(3).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

        [Fact]
        public void BoundaryMediaPositionIncrease()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn1Before = imagesBefore.ElementAt(0);
            var result = mediaRepo.SetMediaPosition(imageAtPosn1Before.MediaId.Value, imagesBefore.Count());

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.True(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(3).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(0).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(1).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(2).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

        [Fact]
        public void BoundaryMediaPositionDecrease()
        {
            var allMedia = mediaRepo.GetMedia(club.ClubId.Value);
            var imagesBefore = allMedia.Where(t => t.MediaType == MediaType.IMAGE).OrderBy(o => o.Position);
            var videosBefore = allMedia.Where(t => t.MediaType == MediaType.VIDEO).OrderBy(o => o.Position);

            var imageAtPosn4Before = imagesBefore.ElementAt(3);
            var result = mediaRepo.SetMediaPosition(imageAtPosn4Before.MediaId.Value, 1);

            var imagesAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.IMAGE).OrderBy(x => x.Position);
            var videosAfter = mediaRepo.GetMedia(club.ClubId.Value).Where(t => t.MediaType == MediaType.VIDEO).OrderBy(x => x.Position);

            Assert.True(result);
            Assert.True(imagesBefore.ElementAt(0).MediaId == imagesAfter.ElementAt(1).MediaId);
            Assert.True(imagesBefore.ElementAt(1).MediaId == imagesAfter.ElementAt(2).MediaId);
            Assert.True(imagesBefore.ElementAt(2).MediaId == imagesAfter.ElementAt(3).MediaId);
            Assert.True(imagesBefore.ElementAt(3).MediaId == imagesAfter.ElementAt(0).MediaId);


            Assert.True(imagesAfter.ElementAt(0).Position == 1);
            Assert.True(imagesAfter.ElementAt(1).Position == 2);
            Assert.True(imagesAfter.ElementAt(2).Position == 3);
            Assert.True(imagesAfter.ElementAt(3).Position == 4);

            Assert.True(videosBefore.ElementAt(0).MediaId == videosAfter.ElementAt(0).MediaId);
            Assert.True(videosBefore.ElementAt(0).Position == videosAfter.ElementAt(0).Position);

            Assert.True(videosBefore.ElementAt(1).MediaId == videosAfter.ElementAt(1).MediaId);
            Assert.True(videosBefore.ElementAt(1).Position == videosAfter.ElementAt(1).Position);

            Assert.True(videosBefore.ElementAt(2).MediaId == videosAfter.ElementAt(2).MediaId);
            Assert.True(videosBefore.ElementAt(2).Position == videosAfter.ElementAt(2).Position);
        }

    }
}