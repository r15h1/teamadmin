using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class PostMedia:Media
    {
        public long? PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
