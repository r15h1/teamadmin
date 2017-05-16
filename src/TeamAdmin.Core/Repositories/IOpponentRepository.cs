using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IOpponentRepository
    {
        Opponent SaveOpponent(Opponent opponent);
        IEnumerable<Opponent> GetOpponents();
        Opponent GetOpponent(int opponentId);
    }
}
