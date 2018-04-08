using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class Type
    {
        public Type()
        {
            CardType = new HashSet<CardType>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<CardType> CardType { get; set; }
    }
}
