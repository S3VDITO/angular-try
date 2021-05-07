namespace HeroesAPI.DAL.Models
{
    using HeroesAPI.DAL.Interfaces;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Hero : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
