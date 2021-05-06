using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using heroes_api.Models;
using heroes_api.Services;

namespace heroes_api.Controllers
{
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroesService _heroesService;

        public HeroesController(HeroesService heroesService)
        {
            _heroesService = heroesService;
        }

        [HttpGet("api/heroes")]
        public ActionResult<IEnumerable<Hero>> GetHeroes()
        {
            return Accepted(_heroesService.Get());
        }

        [HttpPost("api/heroes")]
        public ActionResult<Hero> AddHero(Hero hero)
        {
            _heroesService.Create(hero);
            return Accepted();
        }

        [HttpGet("api/heroes/{id}")]
        public ActionResult<Hero> GetHero(int id)
        {
            var person = _heroesService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
    }
}
