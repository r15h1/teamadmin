using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class Program
    {
        public long? ProgramId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<Media> Media { get; set; }
    }
}
