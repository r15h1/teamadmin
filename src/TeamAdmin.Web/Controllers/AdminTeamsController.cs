using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/teams")]
    public class AdminTeamsController : Controller
    {
        ITeamRepository teamRepository;
        int clubId = 1;
        private IMapper mapper;

        public AdminTeamsController(ITeamRepository teamRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var teamsList = teamRepository.GetTeams();
            return View(teamsList);
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var t = teamRepository.GetTeam(clubId, id);
            var team = mapper.Map<Models.AdminViewModels.Team>(t);
            return View("Details", team);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            var team = new Models.AdminViewModels.Team { ClubId = clubId };
            return View("Details", team);
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.AdminViewModels.Team team)
        {
            if (ModelState.IsValid)
            {
                var tm = new Core.Team(clubId) { Name = team.Name };
                if (team.TeamId.HasValue) tm.TeamId = team.TeamId;
                teamRepository.SaveTeam(tm);
                return RedirectToAction("Index");
            }
            return View("Details", team);
        }
    }
}