using heroes_api.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace heroes_api.Services
{
    public class HeroesService
    {
        private readonly IMongoCollection<Hero> _heroes;

        public HeroesService(IHeroesDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _heroes = database.GetCollection<Hero>(settings.HeroesCollectionName);
        }

        public List<Hero> Get() =>
            _heroes.Find(hero => true).ToList();

        public Hero Get(int id) =>
            _heroes.Find<Hero>(hero => hero.Id == id).FirstOrDefault();

        public Hero Create(Hero hero)
        {
            _heroes.InsertOne(new Hero() { Id = Get().Count + 1, Name = hero.Name });
            return hero;
        }

        public Hero Update(Hero newHero)
        {
            throw new NotImplementedException();
        }

        public Hero Delete(int id)
        {
            Hero hero = Get(id);
            _heroes.DeleteOne(item => item.Id == id);
            return hero;
        }
    }
}
