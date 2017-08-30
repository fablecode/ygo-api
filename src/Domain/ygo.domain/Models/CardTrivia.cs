using System;

namespace ygo.domain.Models
{
    public class CardTrivia
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public string Trivia { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Card Card { get; set; }
    }
}