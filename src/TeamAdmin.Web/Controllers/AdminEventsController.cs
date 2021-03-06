using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/events")]
    public class AdminEventsController : Controller
    {
        IEventRepository eventRepository;
        ITeamRepository teamRepository;
        private Club club;
        private IMapper mapper;
        private IOpponentRepository opponentRepository;

        public AdminEventsController(IEventRepository eventRepository, ITeamRepository teamRepository, IOpponentRepository opponentRepository, IMapper mapper)
        {
            club = new Club { ClubId = 1 };
            this.eventRepository = eventRepository;
            this.teamRepository = teamRepository;
            this.opponentRepository = opponentRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var events = eventRepository.GetEvents(club);
            return View(events);
        }

        [HttpGet("{id}")]
        public IActionResult Details(long id)
        {
            var teams = teamRepository.GetTeams();
            var opponents = opponentRepository.GetOpponents();
            var ev = eventRepository.GetEvent(id);
            var evnt = mapper.Map<Models.AdminViewModels.Event>(ev);
            if (teams != null)
                foreach (var team in teams)
                    evnt.TeamList.Add(new Models.AdminViewModels.Team { ClubId = team.ClubId, Name = $"{team.DisplayName} - {team.Name}", TeamId = team.TeamId });

            if (opponents != null)
                foreach (var opponent in opponents)
                    evnt.OpponentList.Add(new Models.ApiViewModels.Opponent { Name = opponent.Name, OpponentId = opponent.OpponentId });

            return View("Details", evnt);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            var teams = teamRepository.GetTeams();
            var model = new Models.AdminViewModels.Event();

            if (teams != null)
                foreach (var team in teams)
                    model.TeamList.Add(new Models.AdminViewModels.Team { ClubId = team.ClubId, Name = team.Name, TeamId = team.TeamId});

            return View("Details", model);
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.AdminViewModels.Event evnt)
        {
            if (ModelState.IsValid)
            {                
                var ev = mapper.Map<Core.Event>(evnt);                
                eventRepository.SaveEvent(club, ev);
                return RedirectToAction("Index");
            }

            var teams = teamRepository.GetTeams();
            if (teams != null)
                foreach (var team in teams)
                    evnt.TeamList.Add(new Models.AdminViewModels.Team { ClubId = team.ClubId, Name = team.Name, TeamId = team.TeamId });
            return View("Details", evnt);
        }
    }
}