namespace HeroesAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HeroesAPI.DAL.Interfaces;
    using HeroesAPI.DAL.Models;
    using HeroesAPI.SettingsModels;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IRepository<Hero> _heroRepository;

        public HeroesController(IRepository<Hero> heroRepository)
        {
            _heroRepository = heroRepository;
        }

        [HttpGet("api/heroes")]
        public async Task<ActionResult<IEnumerable<Hero>>> GetAll()
        {
            return Accepted(await _heroRepository.GetAll());
        }

        [HttpGet("api/heroes/{id}")]
        public async Task<ActionResult<Hero>> Get(int id)
        {
            var heroes = await _heroRepository.GetById(id);

            if (heroes == null)
            {
                return NotFound();
            }

            return Accepted(heroes);
        }

        [HttpGet("api/heroes/name={name}")]
        public async Task<ActionResult<Hero>> Get(string name)
        {
            return Accepted(await _heroRepository.GetBySubName(name));
        }

        [HttpPost("api/heroes")]
        public async Task<ActionResult<Hero>> Create(Hero hero)
        {
            return Accepted(await _heroRepository.Create(hero));
        }

        [HttpPut("api/heroes")]
        public async Task<ActionResult<Hero>> Update(Hero hero)
        {
            var heroTemp = await _heroRepository.GetById(hero.Id);

            if (heroTemp == null)
            {
                return NotFound();
            }

            return Accepted(await _heroRepository.Update(hero));
        }

        [HttpDelete("api/heroes/{id}")]
        public async Task<ActionResult<Hero>> DeleteAsync(int id)
        {
            var hero = await _heroRepository.GetById(id);

            if (hero == null)
            {
                return NotFound();
            }

            return Accepted(await _heroRepository.Delete(id));
        }
    }
}
