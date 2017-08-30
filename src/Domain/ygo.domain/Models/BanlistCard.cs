namespace ygo.domain.Models
{
    public class BanlistCard
    {
        public long BanlistId { get; set; }
        public long CardId { get; set; }
        public long LimitId { get; set; }

        public Banlist Banlist { get; set; }
        public Card Card { get; set; }
        public Limit Limit { get; set; }
    }
}