namespace HeroesAPI.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using HeroesAPI.DAL.Interfaces;
    using HeroesAPI.SettingsModels;

    using MongoDB.Driver;

    public class MongoDbContext<TEntity> : IDbContext<TEntity>
        where TEntity : IEntity
    {
        private readonly string _tableName;

        public MongoDbContext(IDatabaseSettings databaseSettings, string tableName)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            Database = client.GetDatabase(databaseSettings.DatabaseName);

            _tableName = tableName;
        }

        public IMongoDatabase Database { get; }

        public async Task Create(TEntity entity)
        {
            await Database.GetCollection<TEntity>(_tableName).InsertOneAsync(entity);
        }

        public async Task Delete(int id)
        {
            await Database.GetCollection<TEntity>(_tableName).DeleteOneAsync(item => item.Id == id);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await Database.GetCollection<TEntity>(_tableName).FindAsync(predicate);
            return await result.ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            await Database
                .GetCollection<TEntity>(_tableName)
                .FindOneAndUpdateAsync(
                    h => h.Id == entity.Id,
                    Builders<TEntity>.Update.Set(h => h, entity));
        }
    }
}
