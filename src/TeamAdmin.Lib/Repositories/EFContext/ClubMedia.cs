using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class ClubMedia : Media
    {
        public int ClubId { get; set; }        
    }
}
