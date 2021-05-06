namespace HeroesAPI.Services
{
    using System;
    using System.Collections.Generic;

    using HeroesAPI.Models;

    using MongoDB.Driver;

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
            // TODO: починить костыль и сделать автоинкремент!
            _heroes.InsertOne(new Hero() { Id = new Random().Next(0, int.MaxValue), Name = hero.Name });
            return hero;
        }

        public Hero Update(Hero hero)
        {
            _heroes.FindOneAndUpdate(h => h.Id == hero.Id, Builders<Hero>.Update.Set(h => h.Name, hero.Name));
            return hero;
        }

        public Hero Delete(int id)
        {
            Hero hero = Get(id);
            _heroes.DeleteOne(item => item.Id == id);
            return hero;
        }
    }
}
