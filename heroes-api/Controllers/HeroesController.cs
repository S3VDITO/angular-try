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
            var person = _heroesService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Accepted(person);
        }

        [HttpPost("api/heroes")]
        public ActionResult<Hero> AddHero(Hero hero)
        {
            return Accepted(_heroesService.Create(hero));
        }

        /// <summary>
        /// Пока не имеет реализацию...
        /// </summary>
        /// <param name="hero">Обновленный герой.</param>
        /// <returns>Результат запроса.</returns>
        [HttpPut("api/heroes")]
        public ActionResult<Hero> UpdateHero(Hero hero)
        {
            return Accepted(_heroesService.Update(hero));
        }

        [HttpDelete("api/heroes/{id}")]
        public ActionResult<Hero> DeleteHero(int id)
        {
            _heroesService.Delete(id);
            return Accepted();
        }
    }
}
