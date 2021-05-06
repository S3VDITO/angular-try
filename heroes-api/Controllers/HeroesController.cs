namespace HeroesAPI.Controllers
{
    using System.Collections.Generic;

    using HeroesAPI.Models;
    using HeroesAPI.Services;

    using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("api/heroes/{id}")]
        public ActionResult<Hero> GetHero(int id)
        {
            var heroes = _heroesService.Get(id);

            if (heroes == null)
            {
                return NotFound();
            }

            return Accepted(heroes);
        }

        [HttpGet("api/heroes/name={name}")]
        public ActionResult<Hero> GetHero(string name)
        {
            return Accepted(_heroesService.Get(name));
        }

        [HttpPost("api/heroes")]
        public ActionResult<Hero> AddHero(Hero hero)
        {
            return Accepted(_heroesService.Create(hero));
        }

        [HttpPut("api/heroes")]
        public ActionResult<Hero> UpdateHero(Hero hero)
        {
            var heroes = _heroesService.Get(hero.Id);

            if (heroes == null)
            {
                return NotFound();
            }

            return Accepted(_heroesService.Update(hero));
        }

        [HttpDelete("api/heroes/{id}")]
        public ActionResult<Hero> DeleteHero(int id)
        {
            var heroes = _heroesService.Get(id);

            if (heroes == null)
            {
                return NotFound();
            }

            _heroesService.Delete(id);

            return Accepted();
        }
    }
}
