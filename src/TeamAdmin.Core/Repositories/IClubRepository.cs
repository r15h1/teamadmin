using System.Collections.Generic;

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