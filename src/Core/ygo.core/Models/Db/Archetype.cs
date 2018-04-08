using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class Archetype
    {
        public Archetype()
        {
            ArchetypeCard = new HashSet<ArchetypeCard>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<ArchetypeCard> ArchetypeCard { get; set; }
    }
}
