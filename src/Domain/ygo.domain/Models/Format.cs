using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Format
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<Banlist> Banlists { get; set; } = new HashSet<Banlist>();
    }
}