using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardType
    {
        public long TypeId { get; set; }
        public long CardId { get; set; }

        public Card Card { get; set; }
        public Type Type { get; set; }
    }
}
