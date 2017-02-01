using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models;

namespace TeamAdmin.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int clubId = 1;
        private IPostRepository postRepository;
        private IEventRepository eventRepository;

        public HomeController(IPostRepository postRepository, IEventRepository eventRepository)
        {
            this.postRepository = postRepository;
            this.eventRepository = eventRepository;
        }

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

        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
