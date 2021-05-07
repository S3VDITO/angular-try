namespace HeroesAPI.Controllers
{
    using System;
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

        [HttpGet("api/heroes/{guid}")]
        public async Task<ActionResult<Hero>> Get(Guid guid)
        {
            var heroes = await _heroRepository.GetById(guid);

            if (heroes == null)
            {
                return NotFound();
            }

            return Accepted(heroes);
        }

        [HttpGet("api/heroes/name={name}")]
        public async Task<ActionResult<Hero>> Get(string name)
        {
            var result = await _heroRepository.GetBySubName(name);
            return Accepted(result);
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

            await _heroRepository.Update(hero);

            return Accepted();
        }

        [HttpDelete("api/heroes/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid guid)
        {
            var hero = await _heroRepository.GetById(guid);

            if (hero == null)
            {
                return NotFound();
            }

            await _heroRepository.Delete(guid);

            return Accepted();
        }
    }
}
