using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Web.Models.ApiViewModels
{
    public class GameResult
    {
        public long EventId { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }
}
