using System;
using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IClubRepository
    {
        Club Save(Club club);
        int Count { get; }
        IEnumerable<Club> Get();
        Club Get(int clubId);
        bool Delete(int clubId);
    }
}