using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeamAdmin.Core.Repositories;
using System;
using System.Collections.Generic;
using TeamAdmin.Core;

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
                var tm = new Core.Team(clubId) { Name = team.Name, DisplayName = team.DisplayName };
                if (team.TeamId.HasValue) tm.TeamId = team.TeamId;

                tm.Media = GetMedia(team.Images, team.Uniforms);
                teamRepository.SaveTeam(tm);
                return RedirectToAction("Index");
            }
            return View("Details", team);
        }

        private IEnumerable<Media> GetMedia(IEnumerable<string> images, IEnumerable<string> uniforms)
        {
            var list = new List<Media>();
            if (images != null && images.Count() > 0)
                for (int i = 1; i <= images.Count(); i++)
                    list.Add(new Media { MediaType = MediaType.PICTURE, Position = i, Url = images.ElementAt(i - 1) });

            if (uniforms != null && uniforms.Count() > 0)
                for (int i = 1; i <= uniforms.Count(); i++)
                    list.Add(new Media { MediaType = MediaType.UNIFORM, Position = i, Url = uniforms.ElementAt(i - 1) });

            return list;
        }
    }
}