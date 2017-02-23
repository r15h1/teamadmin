using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models;
using System;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using System.Text;
using AutoMapper;
using TeamAdmin.Lib.Util;

namespace TeamAdmin.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private const int clubId = 1;
        private IPostRepository postRepository;
        private IEventRepository eventRepository;
        private IClubRepository clubRepository;
        private IMapper mapper;

        public HomeController(IClubRepository clubRepository, IPostRepository postRepository, IEventRepository eventRepository, IMapper mapper)
        {
            this.clubRepository = clubRepository;
            this.postRepository = postRepository;
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var news = postRepository.GetPosts(clubId);
            var events = eventRepository.GetEvents(new Core.Club { ClubId = clubId });
            var model = new HomePageModel {
                Events = events.GroupBy(e => e.StartDate.Date, e => e).OrderBy(k => k.Key),
                News = news
            };
            return View(model);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public async Task<IActionResult> Contact(Message message)
        {
            if (!ModelState.IsValid) return  View(message);
         
            var verified = await IsCaptchaVerified();
            if (!verified)
            {
                ModelState.AddModelError("", "Captcha image verification failed.");
                return View(message);
            }

            var msg = mapper.Map<Core.Message>(message);
            msg.DateCreated = DateTime.UtcNow;
            var savedMsg = clubRepository.SaveMessage(msg);
            return View(mapper.Map<Web.Models.Message>(savedMsg));
        }

        private async Task<bool> IsCaptchaVerified()
        {
            string userIP = string.Empty;
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            if (ipAddress != null)
            {
                userIP = ipAddress.MapToIPv4().ToString();
            }

            var captchaImage = Request.Form["g-recaptcha-response"];
            var postData = string.Format("&secret={0}&remoteip={1}&response={2}", Settings.GoogleRecaptchaSecret, userIP, Request.Form["g-recaptcha-response"]);
            var postDataAsBytes = Encoding.UTF8.GetBytes(postData);

            WebClient webClient = new WebClient();
            webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            var json = await webClient.UploadStringTaskAsync
            (new System.Uri("https://www.google.com/recaptcha/api/siteverify"), "POST", postData);
            return JsonConvert.DeserializeObject<CaptchaResponse>(json).Success;
        }


        [Authorize]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
