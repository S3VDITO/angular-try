namespace HeroesAPI.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Driver;

    public class Hero
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
