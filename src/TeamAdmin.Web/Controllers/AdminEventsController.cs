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
        private Club club;
        private IMapper mapper;

        public AdminEventsController(IEventRepository eventRepository, IMapper mapper)
        {
            club = new Club { ClubId = 890 };
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var events = eventRepository.GetEvents(club);
            return View(events);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View("Details");
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.AdminViewModels.Event evnt)
        {
            if (ModelState.IsValid)
            {                
                var ev = mapper.Map<Core.Event>(evnt);
                eventRepository.CreateEvent(club, ev);
                return RedirectToAction("Index");
            }
            return View("Details", evnt);
        }
    }
}