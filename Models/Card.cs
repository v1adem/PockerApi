namespace PockerApi.Models
{
    public class Card
    {
        public string Suit { get; set; } = null!;
        public string Rank { get; set; } = null!;
        public bool IsBlocked { get; set; } = false;
    }
}