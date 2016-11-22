using AutoMapper;
using System;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;
using System.Collections.Generic;

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
                var p1 = context.Posts.SingleOrDefault(p => p.PostId == postId);
                if (p1 != null) return mapper.Map<Core.Post>(p1);
            }
            return null;
        }

        public IEnumerable<Core.Post> GetPosts(int clubId)
        {
            using (var context = ContextFactory.Create<PostContext>())
            {
                List<EFContext.Post> posts = context.Posts.Where(p => p.ClubId == clubId).ToList();
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
                var p = context.Posts.FirstOrDefault(x => x.PostId == post.PostId);
                if (p == null) return null;

                p.Body = post.Body;
                p.DatePublished = post.DatePublished;
                p.PostStatus = (byte) post.PostStatus;
                p.Title = post.Title;
                context.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return mapper.Map<Core.Post>(p);
            }
        }
    }
}
