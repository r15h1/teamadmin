using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Lib.Repositories;
using TeamAdmin.Lib.zz;

namespace TeamAdmin.Web.Controllers
{
    /// <summary>
    /// temporary controller - will be removed when concept of pages added
    /// </summary>
    [Route("")]
    public class zzController : Controller
    {
        zzRepository repository;
        public zzController()
        {
            repository = new zzRepository();
        }


        [HttpGet("tryout-application")]
        public IActionResult TryOut()
        {
            return View();
        }

        [HttpPost("tryout-application")]
        public IActionResult TryOut(TryOutModel model)
        {
            repository.Save(model);
            return View(model);
        }

        [HttpGet("summer-camp-registration")]
        public IActionResult SummerCampRegistration()
        {
            return View();
        }

        [HttpPost("summer-camp-registration")]
        public IActionResult SummerCampRegistration(SummerCampRegistration model)
        {
            repository.Save(model);
            return View(model);
        }

        [HttpGet("2006-to-2009")]
        public IActionResult Register()
        {
            return View();
        }
    }
}