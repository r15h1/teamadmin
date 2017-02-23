using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/contacts")]
    public class AdminContactsController : Controller
    {
        private IClubRepository clubRepository;

        public AdminContactsController(IClubRepository clubRepository)
        {
            this.clubRepository = clubRepository;
        }

        public IActionResult Index()
        {
            var messages = clubRepository.GetMessages();
            return View(messages);
        }
    }
}