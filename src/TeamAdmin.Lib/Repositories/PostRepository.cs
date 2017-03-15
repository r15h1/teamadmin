using AutoMapper;
using System;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TeamAdmin.Lib.Repositories
{
    public class PostRepository : IPostRepository
    {
        IMapper mapper;

        public PostRepository()
        {
            mapper = AutoMapperFactory.GetMapper();
        }

        public bool DeletePost(long postId)
        {
            using (var context = ContextFactory.Create<PostContext>())
            {
                var p1 = context.Posts.SingleOrDefault(p => p.PostId == postId);
                if (p1 == null) return false;
                context.Entry(p1).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();
            }
            return true;
        }

        public Core.Post GetPost(long postId)
        {
            using (var context = ContextFactory.Create<PostContext>())
            {
                var p1 = context.Posts.Include(m => m.PostMedia).SingleOrDefault(p => p.PostId == postId);
                if (p1 != null) return mapper.Map<Core.Post>(p1);
            }
            return null;
        }

        public IEnumerable<Core.Post> GetPosts(int clubId)
        {
            using (var context = ContextFactory.Create<PostContext>())
            {
                List<EFContext.Post> posts = context.Posts.Include(m => m.PostMedia).Where(p => p.ClubId == clubId).OrderByDescending(p => p.DatePublished).ToList();
                if (posts != null && posts.Count() > 0)
                    return mapper.Map<List<Core.Post>>(posts);
            }
            return null;
        }

        public Core.Post SavePost(Core.Post post)
        {
            if (post.PostId.HasValue)
                return UpdatePost(post);

            return CreatePost(post);
        }

        private Core.Post CreatePost(Core.Post post)
        {
            if (post.PostStatus == PostStatus.PUBLISHED) post.DatePublished = DateTime.UtcNow;
            var p = mapper.Map<EFContext.Post>(post);
            using (var context = ContextFactory.Create<PostContext>())
            {
                context.Posts.Add(p);
                context.SaveChanges();
                return mapper.Map<Core.Post>(p);
            }
        }

        private Core.Post UpdatePost(Core.Post post)
        {
            using (var context = ContextFactory.Create<PostContext>())
            {
                var p = context.Posts.Include(m => m.PostMedia).FirstOrDefault(x => x.PostId == post.PostId);
                if (p == null) return null;

                p.Body = post.Body;
                if (p.PostStatus != (byte)post.PostStatus && post.PostStatus == PostStatus.PUBLISHED) p.DatePublished = DateTime.UtcNow;

                p.PostStatus = (byte) post.PostStatus;
                p.Title = post.Title;

                if (p.PostMedia != null && p.PostMedia.Count > 0)
                    foreach (var m in p.PostMedia)
                        context.Remove(m);

                if (post.Media != null && post.Media.Count() > 0)
                    foreach (var media in post.Media)
                        p.PostMedia.Add(new PostMedia {
                            Caption = media.Caption, MediaId = media.MediaId, MediaType = (byte)media.MediaType,
                            Position = media.Position, Url = media.Url, PostId = post.PostId
                        });

                context.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return mapper.Map<Core.Post>(p);
            }
        }
    }
}
