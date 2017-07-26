using System;
using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface ICompetitionsRepository
    {
        Competition GetCompetition(long id);
        Competition SaveCompetition(Competition competition);
        bool DeleteCompetition(long id);
        IEnumerable<Competition> GetCompetitions(string name = "");
    }
}