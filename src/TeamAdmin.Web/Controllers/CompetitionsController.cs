using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeamAdmin.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Competitions")]
    public class CompetitionsController : Controller
    {
        private IMapper mapper;
        public CompetitionsController(IOpponentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
    }
}