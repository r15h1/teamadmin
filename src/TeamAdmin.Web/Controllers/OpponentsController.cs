using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/opponents")]
    public class OpponentsController : Controller
    {
        private IMapper mapper;
        private IOpponentRepository repository;

        public OpponentsController(IOpponentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromForm][FromBody] Models.ApiViewModels.Opponent opponent)
        {
            if (!ModelState.IsValid) return StatusCode(400, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            if(opponent.OpponentId.HasValue) return StatusCode(400, "To create opponents, use the POST request. For update, use PUT request and supply an opponent id");

            try
            {
                var opp = mapper.Map<TeamAdmin.Core.Opponent>(opponent);
                var newOpponent = repository.SaveOpponent(opp);
                return StatusCode(201, newOpponent);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Models.ApiViewModels.Opponent opponent)
        {
            if (!ModelState.IsValid) return StatusCode(400, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            if (!opponent.OpponentId.HasValue) return StatusCode(400, "To create opponents, use the POST request. For update, use PUT request and supply an opponent id");

            try
            {
                var opp = mapper.Map<TeamAdmin.Core.Opponent>(opponent);
                var updatedOpponent = repository.SaveOpponent(opp);
                return StatusCode(200, updatedOpponent);
            }
            catch (ArgumentNullException ex) { return StatusCode(400, ex.Message); }
            catch (ArgumentException ex) { return StatusCode(404, ex.Message); }            
            catch (Exception ex) {return StatusCode(500, ex.Message);}
        }

        [HttpGet]
        public IActionResult Get()
        {
            var opponents = repository.GetOpponents();
            return StatusCode(200, opponents);
        }

        [HttpGet("{opponentId:int}")]
        public IActionResult Get(int opponentId)
        {
            var opponent = repository.GetOpponent(opponentId);
            if (opponent == null) return StatusCode(404);

            return StatusCode(200, opponent);
        }
    }
}