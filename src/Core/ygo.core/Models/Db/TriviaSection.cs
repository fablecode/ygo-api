using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class TriviaSection
    {
        public TriviaSection()
        {
            Trivia = new HashSet<Trivia>();
        }

        public long Id { get; set; }
        public long CardId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Card Card { get; set; }
        public ICollection<Trivia> Trivia { get; set; }
    }
}