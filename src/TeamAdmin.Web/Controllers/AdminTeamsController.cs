using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models.AdminViewModels;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/teams")]
    public class AdminTeamsController : Controller
    {
        private ITeamRepository teamRepository;
        private IPlayerRepository playerRepository;
        private IMapper mapper;
        private int clubId = 1;

        public AdminTeamsController(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            this.teamRepository = teamRepository;
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Teams()
        {
            var teamsList = teamRepository.GetTeams();
            return View("Teams", teamsList);
        }

        [HttpGet("{teamid}")]
        public IActionResult TeamDetails(int teamId)
        {
            var t = teamRepository.GetTeam(teamId);
            var team = mapper.Map<Models.AdminViewModels.Team>(t);
            return View("TeamDetails", team);
        }

        [HttpPost("{teamid}")]
        [ValidateAntiForgeryToken]
        public IActionResult TeamDetails(Models.AdminViewModels.Team team)
        {
            if (ModelState.IsValid)
            {
                var tm = new Core.Team(clubId) { Name = team.Name, DisplayName = team.DisplayName };
                if (team.TeamId.HasValue) tm.TeamId = team.TeamId;

                tm.Media = GetMedia(team.Images, team.Uniforms);
                teamRepository.SaveTeam(tm);
                return RedirectToAction("Teams");
            }
            return View("TeamDetails", team);
        }

        [HttpGet("add")]
        public IActionResult NewTeam()
        {
            var team = new Models.AdminViewModels.Team { ClubId = clubId };
            return View("TeamDetails", team);
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult NewTeam(Models.AdminViewModels.Team team)
        {
            if (ModelState.IsValid)
            {
                var tm = new Core.Team(clubId) { Name = team.Name, DisplayName = team.DisplayName };
                if (team.TeamId.HasValue) tm.TeamId = team.TeamId;

                tm.Media = GetMedia(team.Images, team.Uniforms);
                teamRepository.SaveTeam(tm);
                return RedirectToAction("Teams");
            }
            return View("TeamDetails", team);
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

        [HttpGet("{teamid}/players")]
        public IActionResult Players(int teamId)
        {
            var team = teamRepository.GetTeam(teamId);
            var players = playerRepository.GetPlayers(teamId);
            var model = new PlayersListModel
            {
                Team = team,
                Players = players
            };
            return View("Players", model);
        }

        [HttpGet("{teamid}/players/add")]
        public IActionResult NewPlayer(int teamId)
        {
            var team = teamRepository.GetTeam(teamId);
            ViewData["Team"] = $"{team.Name}{(!string.IsNullOrWhiteSpace(team.DisplayName) ? " - " + team.DisplayName : "")}";
            return View("PlayerDetails", null);
        }

        [HttpPost("{teamid}/players/add")]
        public IActionResult NewPlayer(Models.AdminViewModels.Player player)
        {
            if (ModelState.IsValid)
            {
                var p = mapper.Map<Core.Player>(player);
                playerRepository.SavePlayer(p);
                return RedirectToAction("Players", new { teamid = player.TeamId });
            }
            return View("PlayerDetails", player);
        }

        [HttpGet("{teamid}/players/{playerid}")]
        public IActionResult PlayerDetails(int playerid)
        {
            var player = playerRepository.GetPlayer(playerid);
            var p = mapper.Map<Models.AdminViewModels.Player>(player);                            
            return View("PlayerDetails", p);
        }

        [HttpPost("{teamid}/players/{playerid}")]
        [ValidateAntiForgeryToken]
        public IActionResult PlayerDetails(Models.AdminViewModels.Player player)
        {
            if (ModelState.IsValid)
            {
                var p = mapper.Map<Core.Player>(player);
                playerRepository.SavePlayer(p);
                return RedirectToAction("Players", new { teamid = player.TeamId });
            }
            return View("PlayerDetails", player);
        }
    }
}