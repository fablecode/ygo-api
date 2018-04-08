using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class ArchetypeCard
    {
        public long ArchetypeId { get; set; }
        public long CardId { get; set; }

        public Archetype Archetype { get; set; }
        public Card Card { get; set; }
    }
}
