using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PockerApi.Models
{
    public class Deck
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public Deck()
        {
            string[] suits = { "hearts", "diamonds", "clubs", "spades" };
            string[] ranks = { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            foreach (var suit in suits)
                foreach (var rank in ranks)
                    Cards.Add(new Card() { Suit = suit, Rank = rank });
        }
        public void ShuffleDeck()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                int j = new Random().Next(Cards.Count);
                (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
            }
        }

        public Card NextCard()
        {
            var card = Cards.First();
            Cards.Remove(card);

            return card;
        }
    }
}