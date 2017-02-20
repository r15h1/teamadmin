using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models;

namespace TeamAdmin.Web.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private ITeamRepository teamRepository;
        private IEventRepository eventRepository;
        private IMapper mapper;

        public EventsController(ITeamRepository teamRepository, IEventRepository eventRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var teams = new List<Team>() { new Team(1) };
            teams.AddRange(teamRepository.GetTeams().ToList());
            var model = new EventsCalendarModel {
                Teams = teams,
                EventTypes = Enum.GetNames(typeof(Core.EventType)).ToList()
            };
            return View(model);
        }

        [HttpGet("{eventId}")]
        public IActionResult Details(long eventId)
        {
            var ev = eventRepository.GetEvent(eventId);
            return View("Details", ev);
        }
    }
}