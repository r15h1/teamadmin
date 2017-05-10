using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Util;
using TeamAdmin.Web.Models;
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

        [HttpPost("image")]
        public async Task<IActionResult> DeleteImage()
        {
            return new JsonResult(new StringContent("ok"));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFileCollection files)
        {
            UploadedData uploadedData = new UploadedData();            
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
                        var url = $"{Settings.ImageUrlRoot}{DateTime.Today.ToString("yyyy-MM")}/{filename}";
                        uploadedData.InitialPreview.Add($"<img src='{url}'>");
                        uploadedData.InitialPreviewConfig.Add(new InitialPreviewConfig { Caption = filename, Key = $"{url}", Url = "/api/image", Extra = new { Id = $"{url}" } });
                    }
                }
            }
            return new JsonResult(uploadedData);
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
            if (!team.HasValue || team.Value <= 0) return new JsonResult(null);

            var events = eventRepository.GetEvents(new Team(1) { TeamId = team.Value }).Select(e => new {
                start = e.StartDate, end = e.EndDate, id = e.EventId,
                title = $"{e.EventType.ToString()}: {e.Title}",
                description = e.Description,
                location = e.Address,
                url = $"{Settings.SiteUrl}events/{e.EventId}",
                className = "event"
            });
            return new JsonResult(events);
        }

        [HttpPost("recaptcha")]
        public async Task<CaptchaVerification> ReCaptcha(ReCaptcha recaptcha)
        {
            if (!ModelState.IsValid) return new CaptchaVerification { Errors = { "Invalid captcha" }, Success = false };

            var verification = await IsCaptchaVerified(recaptcha);
            return verification;
        }

        private async Task<CaptchaVerification> IsCaptchaVerified(ReCaptcha recaptcha)
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null) userIP = ipAddress.MapToIPv4().ToString();

            var payload = string.Format("&secret={0}&remoteip={1}&response={2}",      
               Settings.GoogleRecaptchaSecret,
               userIP,
               recaptcha.CaptchaResponse
            );

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://www.google.com");
            var request = new HttpRequestMessage(HttpMethod.Post, "/recaptcha/api/siteverify");
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.SendAsync(request);
            return JsonConvert.DeserializeObject<CaptchaVerification>(response.Content.ReadAsStringAsync().Result); ;
        }

    }
}