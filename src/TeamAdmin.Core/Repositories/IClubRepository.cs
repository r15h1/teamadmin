using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core.Repositories
{
    public interface IClubRepository
    {
        Club Save(Club club);
        int Count { get; }
        IEnumerable<Club> Get();
        bool Delete(int clubId);
    }
}