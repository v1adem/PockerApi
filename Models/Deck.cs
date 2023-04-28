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
            //string[] suits = { "hearts", "diamonds", "clubs", "spades" };
            //string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            int[] suits = { 1, 2, 3, 4}; // "hearts" = 1, "diamonds" = 2, "clubs" = 3, "spades" = 4
            int[] ranks = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}; // 2 = 2, ... , J = 11, Q = 12, K = 13, A = 14

            foreach (var suit in suits)
                foreach (var rank in ranks)
                    Cards.Add(new Card(suit, rank));
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