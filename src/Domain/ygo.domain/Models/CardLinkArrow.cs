namespace ygo.domain.Models
{
    public class CardLinkArrow
    {
        public long LinkArrowId { get; set; }
        public long CardId { get; set; }

        public Card Card { get; set; }
        public LinkArrow LinkArrow { get; set; }
    }
}