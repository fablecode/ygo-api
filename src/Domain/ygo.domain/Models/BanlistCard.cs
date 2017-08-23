namespace ygo.domain.Models
{
    public class BanlistCard
    {
        public long BanlistId { get; set; }

        public long CardId { get; set; }

        public long LimitId { get; set; }

        public virtual Banlist Banlist { get; set; }

        public virtual Card Card { get; set; }

        public virtual Limit Limit { get; set; }
    }
}