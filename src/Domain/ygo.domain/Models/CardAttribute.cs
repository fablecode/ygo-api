namespace ygo.domain.Models
{
    public class CardAttribute
    {
        public long AttributeId { get; set; }
        public long CardId { get; set; }

        public Attribute Attribute { get; set; }
        public Card Card { get; set; }
    }
}