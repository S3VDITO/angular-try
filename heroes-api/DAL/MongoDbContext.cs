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

        public Task Create(TEntity entity)
        {
            return Database.GetCollection<TEntity>(_tableName).InsertOneAsync(entity);
        }

        public Task Delete(Guid guid)
        {
            return Database.GetCollection<TEntity>(_tableName).DeleteOneAsync(item => item.Id == guid);
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Database.GetCollection<TEntity>(_tableName).Find(predicate).ToListAsync();
        }

        public Task Update(TEntity entity)
        {
            return Database
                .GetCollection<TEntity>(_tableName)
                .FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
        }
    }
}
