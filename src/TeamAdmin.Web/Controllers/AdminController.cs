using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : Controller
    {
        IPostRepository postRepository;
        int clubId = 1;
        private IMapper mapper;

        public AdminController(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet("News")]
        public IActionResult News()
        {
            var newsList = postRepository.GetPosts(clubId);
            return View(newsList);
        }

        [HttpGet("News/Add")]
        public IActionResult AddNews()
        {
            return View("NewsDetails");
        }

        [HttpPost("News/Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddNews(Models.AdminViewModels.News news)
        {
            if (ModelState.IsValid)
            {
                news.ClubId = clubId;
                var post = mapper.Map<Core.Post>(news);
                postRepository.SavePost(post);
                return RedirectToAction("News");
            }
            return View("NewsDetails", news);
        }
    }
}