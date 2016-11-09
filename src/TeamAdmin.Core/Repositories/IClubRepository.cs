using System;
using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IClubRepository
    {
        Club SaveClub(Club club);
        int ClubCount { get; }
        IEnumerable<Club> GetClubs();
        Club GetClub(int clubId);
        bool DeleteClub(int clubId);
    }
}