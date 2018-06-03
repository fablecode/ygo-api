using System;

namespace ygo.core.Models.Db
{
    public class Trivia
    {
        public long Id { get; set; }
        public long TriviaSectionId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public TriviaSection TriviaSection { get; set; }
    }
}