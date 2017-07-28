using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core
{
    public class GameResult
    {
        public GameResult() { }
        public GameResult(string delimitedResult)
        {
            if(!string.IsNullOrWhiteSpace(delimitedResult) && delimitedResult.Contains("|"))
            {
                int team1score, team2score;
                string[] split = delimitedResult.Split(new char[] { '|' });
                if(split.Length > 1)
                {
                    if (int.TryParse(split[0], out team1score) && int.TryParse(split[1], out team2score))
                    {
                        Team1Score = team1score;
                        Team2Score = team2score;
                    }
                }
            }
        }

        public int Team1Score { get; set; }
        public long Team2Score { get; set; }
    }
}