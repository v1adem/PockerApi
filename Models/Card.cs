namespace PockerApi.Models
{
    public class Card
    {
        public int Suit { get; set; } = 0; // "hearts" = 1, "diamonds" = 2, "clubs" = 3, "spades" = 4
        public int Rank { get; set; } = 0; // 2 = 2, ... , J = 11, Q = 12, K = 13, A = 14
        public bool IsBlocked { get; set; } = false;

        public Card(int Suit, int Rank) 
        {
            this.Suit = Suit;       
            this.Rank = Rank;
        }
    }
}