using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Archetype
    {
        public long Id { get; set; }

        public string ArchetypeNumber { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<Card> Cards { get; set; } = new HashSet<Card>();
    }
}