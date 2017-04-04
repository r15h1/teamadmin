using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Util;
using TeamAdmin.Web.Models;

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

        [HttpGet("academy-graduates")]
        public IActionResult AcademyGraduates()
        {
            return View();
        }

        [HttpGet("staff")]
        public IActionResult Staff()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(Message message)
        {
            if (!ModelState.IsValid) return  View(message);            

            var msg = mapper.Map<Core.Message>(message);
            msg.MessageType = Core.MessageType.General_Information;
            msg.DateCreated = DateTime.UtcNow;
            var savedMsg = clubRepository.SaveMessage(msg);
            return View(mapper.Map<Web.Models.Message>(savedMsg));
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
