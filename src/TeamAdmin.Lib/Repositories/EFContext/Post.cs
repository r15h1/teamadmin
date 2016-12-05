using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Post
    {
        public Post()
        {

        }

        public int ClubId { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePublished { get; set; }
        public long? PostId { get; set; }
        public byte PostStatus { get; set; }
        public string Title { get; set; }
        public ICollection<PostMedia> PostMedia { get; set; }
    }
}
