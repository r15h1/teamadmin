using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IOpponentRepository
    {
        Opponent SaveOpponent(Opponent opponent);
        IEnumerable<Opponent> GetOpponents(string name = "");
        Opponent GetOpponent(int opponentId);
    }
}
