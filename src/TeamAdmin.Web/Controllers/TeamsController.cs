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
        private const int clubId = 1;

        public TeamsController(ITeamRepository teamRepository, IEventRepository eventRepository)
        {
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
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
            return View(new TeamAdmin.Web.Models.TeamDetailsModel { Team = team, Events = events.GroupBy(e => e.StartDate.Date, e => e).OrderBy(k => k.Key) });
        }

        [Route("{teamid}/{name}/players")]
        public IActionResult Players(int teamid)
        {
            var team = teamRepository.GetTeam(teamid);
            var events = eventRepository.GetEvents(new Core.Team(clubId) { TeamId = teamid });
            return View(new TeamAdmin.Web.Models.TeamDetailsModel { Team = team, Events = events.GroupBy(e => e.StartDate.Date, e => e).OrderBy(k => k.Key) });
        }
    }
}