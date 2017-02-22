using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models;

namespace TeamAdmin.Web.Controllers
{
    [Route("programs")]
    public class ProgramsController : Controller
    {
        private IProgramRepository programRepository;

        public ProgramsController(IProgramRepository programRepository)
        {
            this.programRepository = programRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Core.Program> programs = programRepository.GetPrograms();
            return View(programs);
        }

        [HttpGet("{programId}/{programName}")]
        public IActionResult Details(int programId)
        {
            var program = programRepository.GetProgram(programId);
            return View("Details", program);
        }
    }
}