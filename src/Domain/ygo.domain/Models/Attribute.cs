using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Attribute
    {
        public Attribute()
        {
            CardAttribute = new HashSet<CardAttribute>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<CardAttribute> CardAttribute { get; set; }
    }
}
