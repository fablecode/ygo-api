namespace ygo.core.Models.Db
{
    public class ArchetypeCard
    {
        public long ArchetypeId { get; set; }
        public long CardId { get; set; }

        public Archetype Archetype { get; set; }
        public Card Card { get; set; }
    }
}