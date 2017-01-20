using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    public class HomeController : Controller
    {
        IPostRepository postRepository;
        private const int clubId = 1;

        public HomeController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            var news = postRepository.GetPosts(clubId);
            return View(news);
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
