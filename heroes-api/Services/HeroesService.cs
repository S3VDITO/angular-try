namespace HeroesAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HeroesAPI.Models;

    using MongoDB.Driver;

    public class HeroesService
    {
        private readonly IMongoCollection<Hero> _heroesCollection;

        public HeroesService(IHeroesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _heroesCollection = database.GetCollection<Hero>(settings.HeroesCollectionName);
        }

        public IEnumerable<Hero> Get() =>
            _heroesCollection.Find(hero => true).ToEnumerable();

        public Hero Get(int id) =>
            _heroesCollection.Find<Hero>(hero => hero.Id == id).FirstOrDefault();

        public Hero Create(Hero hero)
        {
            hero = new Hero()
            {
                Id = Get().Aggregate(0, (accum, h) =>
                {
                    if (accum < h.Id)
                    {
                        return h.Id;
                    }

                    return accum;
                }) + 1,
                Name = hero.Name,
            };

            // TODO: починить костыль и сделать нормальный автоинкремент или забить!
            _heroesCollection.InsertOne(hero);

            return hero;
        }

        public Hero Update(Hero hero)
        {
            _heroesCollection.FindOneAndUpdate(h => h.Id == hero.Id, Builders<Hero>.Update.Set(h => h.Name, hero.Name));
            return hero;
        }

        public Hero Delete(int id)
        {
            Hero hero = Get(id);
            _heroesCollection.DeleteOne(item => item.Id == id);
            return hero;
        }
    }
}
