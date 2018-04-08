using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardAttribute
    {
        public long AttributeId { get; set; }
        public long CardId { get; set; }

        public Attribute Attribute { get; set; }
        public Card Card { get; set; }
    }
}
