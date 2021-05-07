namespace HeroesAPI.DAL.Models
{
    using System;

    using HeroesAPI.DAL.Interfaces;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Hero : IEntity
    {
        [BsonId]
        [BsonElement("id")]
        public Guid Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
