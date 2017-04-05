using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Route("news")]
    public class NewsController : Controller
    {
        private IPostRepository postRepository;
        private const int clubId = 1;

        public NewsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            var news = postRepository.GetPosts(clubId, Core.PostStatus.PUBLISHED);
            return View(news);
        }

        [Route("{newsid}/{title}")]
        public IActionResult Details(long newsid,string title)
        {
            var news = postRepository.GetPost(newsid);
            return View(news);
        }
    }
}