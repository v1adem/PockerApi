using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PockerApi.Models
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public float? Money { get; set; }
        public float? Bet { get; set; } = null!;

        public List<Card> Cards { get; set; } = new List<Card>(5);

        public float CheckCombinations()
        {

            List<int> ranks = new List<int>();
            List<int> suits = new List<int>();
            foreach (Card card in Cards)
            {
                ranks.Add(card.Rank);
                suits.Add(card.Suit);
            }

            int minRank = ranks.Min();
            // Flush
            if ((suits[0] == suits[1]) && (suits[1] == suits[2]) && (suits[2] == suits[3]) && (suits[3] == suits[4])) 
            {
                // Flush Royal
                if ((ranks.Contains(14)) && (ranks.Contains(13)) && (ranks.Contains(12)) && (ranks.Contains(11)) && (ranks.Contains(10)))
                {
                    return 100;
                }
                // Stright Flush
                if ((ranks.Contains(minRank + 1)) && (ranks.Contains(minRank + 2)) && (ranks.Contains(minRank + 3)) && (ranks.Contains(minRank + 4)))
                { 
                    return 10; 
                }

                return 1.75f;
            }

            //Straight
            if ((ranks.Contains(minRank + 1)) && (ranks.Contains(minRank + 2)) && (ranks.Contains(minRank + 3)) && (ranks.Contains(minRank + 4)))
            {
                return 1.75f;
            }

            //Kare
            ranks.Sort();
            for (int i = 0; i < 2; i++)
            {
                if((ranks[i] == ranks[i+1]) && (ranks[i+1] == ranks[i + 2]) && (ranks[i + 2] == ranks[i + 3])) 
                {
                    return 4;
                }
            }

            // Full house
            if ((ranks[0] == ranks[1]) && (ranks[1] == ranks[2]) && (ranks[3] == ranks[4]) || (ranks[2] == ranks[3]) && (ranks[3] == ranks[4]) && (ranks[0] == ranks[1]))
            {
                 return 2;
            }

            //Three
            for (int i = 0; i < 3; i++)
            {
                if ((ranks[i] == ranks[i + 1]) && (ranks[i + 1] == ranks[i + 2]))
                {
                    return 1.5f;
                }
            }

            //Two
            for (int i = 0; i < 3; i++)
            {
                if ((ranks[i] == ranks[i + 1]) && (ranks[i + 1] != ranks[i + 2]))
                {
                    // Two pairs
                    if (i <= 1 && (ranks[i + 2] == ranks[i + 3]))
                    {
                        return 1.4f;
                    }
                    if ((ranks[0] == ranks[1]) && (ranks[3] == ranks[4]))
                    {
                        return 1.4f;
                    }
                    return 1.25f;
                }
            }

            if (ranks[3] == ranks[4]) return 1.25f;

            // No combination
            return 0;
        }
    }
}
