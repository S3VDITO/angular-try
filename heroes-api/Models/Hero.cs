using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace heroes_api.Models
{
    public class Hero
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
