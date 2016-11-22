using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories;
using TeamAdmin.Lib.Tests.Repositories;
using Xunit;

namespace TeamAdmin.Lib.Tests
{
    [Collection("PostFixtureCollection")]
    public class PostsTests
    {
        private Club club;
        private IEnumerable<Team> teams;
        private IPostRepository postRepository;

        public PostsTests(ClubFixture fixture)
        {
            club = fixture.Clubs.FirstOrDefault();
            teams = fixture.Teams.Where(t => t.ClubId == club.ClubId);
            postRepository = new PostRepository();
        }

        [Fact]
        public void CreatePost()
        {
            Post post = BuildPost();
            Post post1 = postRepository.SavePost(post);
            Assert.True(post1.PostId.HasValue);
            AssertEquals(post, post1);
        }

        private void AssertEquals(Post post1, Post post2)
        {
            Assert.True(post2.Body == post1.Body);
            Assert.True(post2.ClubId == post1.ClubId);
            Assert.True(post2.DateCreated.ToString("dd/MM/yyyy hh:mm") == post1.DateCreated.ToString("dd/MM/yyyy hh:mm"));
            Assert.True(post2.DatePublished.ToString("dd/MM/yyyy hh:mm") == post1.DatePublished.ToString("dd/MM/yyyy hh:mm"));
            Assert.True(post2.PostStatus == post1.PostStatus);
            Assert.True(post2.Title == post1.Title);
        }

        [Fact]
        public void UpdatePost()
        {
            Post post = BuildPost();
            Post post1 = postRepository.SavePost(post);
            var post2 = new Post(post1.ClubId) {
                Body = "new body",
                DatePublished = post.DatePublished.AddDays(1),
                PostStatus = PostStatus.PUBLISHED,
                Title = "new title",
                PostId = post1.PostId,
                DateCreated = post1.DateCreated
            };            
            var post3 = postRepository.SavePost(post2);
            AssertEquals(post2, post3);
            Assert.True(post2.PostId == post3.PostId);
        }

        private Post BuildPost()
        {
            return new Post(club.ClubId.Value)
            {
                PostId = null,
                Title = "Post Title",
                Body = "Body",
                DateCreated = DateTime.Now,
                PostStatus = PostStatus.DRAFT,
                DatePublished = DateTime.Now
            };
        }

        [Fact]
        public void GetPostById()
        {
            var post1 = postRepository.SavePost(BuildPost());
            var post2 = postRepository.GetPost(post1.PostId.Value);
            AssertEquals(post1, post2);
            Assert.True(post1.PostId == post2.PostId);
        }

        [Fact]
        public void DeletePost()
        {
            var post1 = postRepository.SavePost(BuildPost());
            bool result = postRepository.DeletePost(post1.PostId.Value);
            Assert.True(result);
            var post2 = postRepository.GetPost(post1.PostId.Value);
            Assert.Null(post2);
        }

        [Fact]
        public void GetPostList()
        {
            IEnumerable<Post> before = postRepository.GetPosts(club.ClubId.Value);
            if (before == null) before = new List<Core.Post>();

            var post1 = postRepository.SavePost(BuildPost());
            var post2 = postRepository.SavePost(BuildPost());
            var post3 = postRepository.SavePost(BuildPost());

            IEnumerable<Post> after = postRepository.GetPosts(club.ClubId.Value);
            var diff = after.Except(before);

            Assert.True(diff.Count() == 3);
            Assert.True(diff.SingleOrDefault(p => p.PostId == post1.PostId) != null);
            Assert.True(diff.SingleOrDefault(p => p.PostId == post2.PostId) != null);
            Assert.True(diff.SingleOrDefault(p => p.PostId == post3.PostId) != null);
        }
    }
}