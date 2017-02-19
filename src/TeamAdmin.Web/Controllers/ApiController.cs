using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Util;
using TeamAdmin.Web.Models.ApiViewModels;

namespace TeamAdmin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        private IHostingEnvironment environment;
        private ITeamRepository teamRepository;
        private IEventRepository eventRepository;

        public ApiController(IHostingEnvironment environment, ITeamRepository teamRepository, IEventRepository eventRepository)
        {
            this.environment = environment;
            this.teamRepository = teamRepository;
            this.eventRepository = eventRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFileCollection files)
        {
            List<string> locs = new List<string>();
            if (files == null || files.Count == 0) files = Request.Form.Files;
            var location = Settings.ImageDirectory + $"\\{DateTime.Today.ToString("yyyy-MM")}";

            if (!Directory.Exists(location)) Directory.CreateDirectory(location);
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    string filename = GenerateFileName(location, file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(location, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        locs.Add($"<img src='{Settings.ImageUrlRoot}{DateTime.Today.ToString("yyyy-MM")}/{filename}'>");
                    }
                }
            }
            return new JsonResult(new UploadedData { InitialPreview = locs });
        }

        private string GenerateFileName(string location, string fileName)
        {
            int counter = 1;
            string tempFileName = fileName;
            while (System.IO.File.Exists(Path.Combine(location, tempFileName)))
            {
                tempFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + counter + Path.GetExtension(fileName);
                counter++;
            }
            return tempFileName;
        }

        [HttpGet("teams")]
        public IActionResult GetTeams()
        {
            var teams = teamRepository.GetTeams();
            return new JsonResult(teams);
        }

        [HttpGet("events")]
        public IActionResult GetEvents(int? team)
        {
            if (!team.HasValue || team.Value <= 0) return null;

            var events = eventRepository.GetEvents(new Team(1) { TeamId = team.Value }).Select(e => new {
                start = e.StartDate, end = e.EndDate, id = e.EventId,
                title = $"{e.EventType.ToString()}: {string.Join(",", e.Teams.Select(t => t.DisplayName))} {(e.EventType == EventType.GAME ? " vs " : " | ")} {e.Title}",
                description = e.Description,
                location = e.Address,
                url = $"{Settings.SiteUrl}events/{e.EventId}",
                className = "event"
            });
            return new JsonResult(events);
        }
    }
}