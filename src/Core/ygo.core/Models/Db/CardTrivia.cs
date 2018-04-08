using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardTrivia
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public string Trivia { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Card Card { get; set; }
    }
}
