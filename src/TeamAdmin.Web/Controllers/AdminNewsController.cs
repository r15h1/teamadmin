using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/news")]
    public class AdminNewsController : Controller
    {
        IPostRepository postRepository;
        int clubId = 1;
        private IMapper mapper;

        public AdminNewsController(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var newsList = postRepository.GetPosts(clubId);
            return View(newsList);
        }
        
        [HttpGet("{id}")]
        public IActionResult Details(long id)
        {
            var post = postRepository.GetPost(id);
            var news = mapper.Map<Models.AdminViewModels.News>(post);
            return View("Details", news);
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View("Details");
        }

        [HttpPost("add")]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Models.AdminViewModels.News news)
        {
            if (ModelState.IsValid)
            {
                news.ClubId = clubId;
                var post = mapper.Map<Core.Post>(news);
                postRepository.SavePost(post);
                return RedirectToAction("Index");
            }
            return View("Details", news);
        }
    }
}