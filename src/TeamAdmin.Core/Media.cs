using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public enum MediaType
    {
        PICTURE = 1,
        VIDEO = 2,
        LOGO = 3,
        UNIFORM = 4
    }

    public class Media
    {
        public long? MediaId { get; set; }
        public MediaType MediaType { get; set; }
        public string Url { get; set; }
        public string Caption { get; set; }
        public int Position { get; set; }
    }
}