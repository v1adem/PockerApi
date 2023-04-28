using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PockerApi.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int? Money { get; set; }
        public int? Bet { get; set; } = null!;

        public List<Card> Cards { get; set; } = new List<Card>();
    }
}