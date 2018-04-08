using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardLinkArrow
    {
        public long LinkArrowId { get; set; }
        public long CardId { get; set; }

        public Card Card { get; set; }
        public LinkArrow LinkArrow { get; set; }
    }
}
