namespace HeroesAPI.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HeroesAPI.DAL.Interfaces;
    using HeroesAPI.DAL.Models;

    using MongoDB.Driver;

    public class HeroRepository : IRepository<Hero>
    {
        private readonly IDbContext<Hero> _heroesDbContext;

        public HeroRepository(IDbContext<Hero> dbContext)
        {
            _heroesDbContext = dbContext;
        }

        public async Task<IEnumerable<Hero>> GetAll() =>
            await _heroesDbContext.Find(x => true);

        public async Task<Hero> GetById(int id)
        {
            var result = await _heroesDbContext.Find(x => x.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Hero>> GetBySubName(string name)
        {
            return await _heroesDbContext.Find(x => x.Name.Contains(name));
        }

        public async Task<Hero> Create(Hero item)
        {
            item = new Hero()
            {
                Id = GetAll().Result.Aggregate(0, (accum, h) =>
                {
                    if (accum < h.Id)
                    {
                        return h.Id;
                    }

                    return accum;
                }) + 1,
                Name = item.Name,
            };

            await _heroesDbContext.Create(item);

            return item;
        }

        public async Task<Hero> Update(Hero item)
        {
            await _heroesDbContext.Update(item);
            return item;
        }

        public async Task Delete(int id)
        {
            await _heroesDbContext.Delete(id);
        }
    }
}
