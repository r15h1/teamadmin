using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Route("teams")]
    public class TeamsController : Controller
    {
        private ITeamRepository teamRepository;
        private IEventRepository eventRepository;
        private IPlayerRepository playerRepository;
        private const int clubId = 1;

        public TeamsController(ITeamRepository teamRepository, IEventRepository eventRepository, IPlayerRepository playerRepository)
        {
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
            this.playerRepository = playerRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.GetTeams();
            return View(teams);
        }

        [Route("{teamid}/{name}")]
        public IActionResult Details(int teamid,string title)
        {
            var team = teamRepository.GetTeam(teamid);
            var events = eventRepository.GetEvents(new Core.Team(clubId) { TeamId = teamid });
            var players = playerRepository.GetPlayers(teamid);
            return View(
                new TeamAdmin.Web.Models.TeamDetailsModel {
                    Team = team,
                    Events = events.GroupBy(e => e.StartDate.Date, e => e).OrderBy(k => k.Key),
                    Players = players
                });
        }        
    }
}