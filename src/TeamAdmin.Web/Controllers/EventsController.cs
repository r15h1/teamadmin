using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private ITeamRepository teamRepository;
        private IEventRepository eventRepository;

        public EventsController(ITeamRepository teamRepository, IEventRepository eventRepository)
        {
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.GetTeams();
            return View(teams);
        }        
    }
}