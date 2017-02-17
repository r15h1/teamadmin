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

        public EventsController(ITeamRepository teamRepository, IEventRepository eventRepository)
        {
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            var teams = new List<Team>() { new Team(1) };
            teams.AddRange(teamRepository.GetTeams().ToList());
            var model = new EventsCalendarModel { Teams = teams, EventTypes = Enum.GetNames(typeof(Core.EventType)).ToList()
        };
            return View(model);
        }        
    }
}