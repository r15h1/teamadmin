using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Web.Models.ApiViewModels;

namespace TeamAdmin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/competitions")]
    public class CompetitionsController : Controller
    {
        private IMapper mapper;
        private readonly ICompetitionsRepository repository;

        public CompetitionsController(ICompetitionsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Competition competition)
        {
            if (!ModelState.IsValid) return StatusCode(400, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            if (competition.CompetitionId.HasValue) return StatusCode(400, "To create competitions, use the POST request. For update, use PUT request and supply an competition id");

            try
            {
                var comp = mapper.Map<TeamAdmin.Core.Competition>(competition);
                var newComp = repository.SaveCompetition(comp);
                return StatusCode(201, mapper.Map<TeamAdmin.Web.Models.ApiViewModels.Competition>(newComp));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Competition competition)
        {
            if (!ModelState.IsValid) return StatusCode(400, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            if (!competition.CompetitionId.HasValue) return StatusCode(400, "To create competitions, use the POST request. For update, use PUT request and supply an competition id");

            try
            {
                var comp = mapper.Map<TeamAdmin.Core.Competition>(competition);
                var updatedCompetition = repository.SaveCompetition(comp);
                return StatusCode(200, mapper.Map<TeamAdmin.Web.Models.ApiViewModels.Competition>(updatedCompetition));
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var competitions = repository.GetCompetitions().OrderBy(o => o.Name);
            return StatusCode(200, mapper.Map<List<TeamAdmin.Web.Models.ApiViewModels.Competition>>(competitions));
        }

        [HttpGet("{competitionId:long}")]
        public IActionResult Get(long competitionId)
        {
            var competition = repository.GetCompetition(competitionId);
            if (competition == null) return StatusCode(404);

            return StatusCode(200, mapper.Map <TeamAdmin.Web.Models.ApiViewModels.Competition>(competition));
        }
    }
}