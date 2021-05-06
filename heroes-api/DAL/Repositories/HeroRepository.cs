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
        private readonly MongoDbContext _dbContext;
        private readonly string _tableName = "Heroes";

        public HeroRepository(MongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Hero>> GetAll() =>
            await _dbContext.Database.GetCollection<Hero>(_tableName).Find(Builders<Hero>.Filter.Empty).ToListAsync();

        public async Task<Hero> GetById(int id) =>
            await _dbContext.Database.GetCollection<Hero>(_tableName).Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Hero>> GetBySubName(string name) =>
            await _dbContext.Database.GetCollection<Hero>(_tableName).Find(x => x.Name.Contains(name)).ToListAsync();

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

            await _dbContext.Database.GetCollection<Hero>(_tableName).InsertOneAsync(item);

            return item;
        }

        public async Task<Hero> Update(Hero item)
        {
            return await _dbContext.Database
                .GetCollection<Hero>(_tableName)
                .FindOneAndUpdateAsync(
                    h => h.Id == item.Id,
                    Builders<Hero>.Update.Set(h => h.Name, item.Name));
        }

        public async Task<DeleteResult> Delete(int id)
        {
            return await _dbContext.Database.GetCollection<Hero>(_tableName).DeleteOneAsync(item => item.Id == id);
        }
    }
}
