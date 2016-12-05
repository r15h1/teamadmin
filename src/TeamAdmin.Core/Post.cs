using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class Post
    {
        public Post(int clubId)
        {
            ClubId = clubId;
            PostStatus = PostStatus.DRAFT;
            Media = new List<Media>();
        }
        public int ClubId { get; private set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePublished { get; set; }
        public long? PostId { get; set; }
        public PostStatus PostStatus { get; set; }
        public string Title { get; set; }
        public IEnumerable<Media> Media { get; set; }
    }

    public enum PostStatus
    {
        DRAFT = 1,
        PUBLISHED = 2
    }
}
