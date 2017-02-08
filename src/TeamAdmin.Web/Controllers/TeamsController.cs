using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Route("teams")]
    public class TeamsController : Controller
    {
        private ITeamRepository teamRepository;
        private const int clubId = 1;

        public TeamsController(ITeamRepository teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.GetTeams();
            return View(teams);
        }

        [Route("{teamid}/{name}")]
        public IActionResult Details(int teamid,string title)
        {
            var team = teamRepository.GetTeam(clubId, teamid);
            return View(team);
        }
    }
}