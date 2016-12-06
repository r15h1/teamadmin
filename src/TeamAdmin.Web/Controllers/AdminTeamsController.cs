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

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View("Details");
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.AdminViewModels.Team team)
        {
            if (ModelState.IsValid)
            {
                team.ClubId = clubId;
                var tm = new Core.Team(clubId) { Name = team.Name };
                teamRepository.SaveTeam(tm);
                return RedirectToAction("Index");
            }
            return View("Details", team);
        }
    }
}