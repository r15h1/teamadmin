using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Authorize]
    [Route("admin/players")]
    public class AdminPlayersController : Controller
    {
        private ITeamRepository temRepository;
        private IPlayerRepository playerRepository;
        private IMapper mapper;

        public AdminPlayersController(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper)
        {
            this.temRepository = teamRepository;
            this.playerRepository = playerRepository;
            this.mapper = mapper;
        }


        [Route("{playerId:int}")]
        public ActionResult Details(int playerId)
        {
            return View("Details");
        }


        [Route("add")]
        public ActionResult Add()
        {
            return View("Details");
        }

        // POST: AdminPlayers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPlayers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminPlayers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminPlayers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPlayers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}