using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core.Repositories
{
    public interface IPostRepository
    {
        Post SavePost(Post post);
        Post GetPost(long value);
        bool DeletePost(long value);
        IEnumerable<Post> GetPosts(int value);
    }
}
