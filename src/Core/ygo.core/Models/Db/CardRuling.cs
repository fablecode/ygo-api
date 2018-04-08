using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardRuling
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public string Ruling { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Card Card { get; set; }
    }
}
