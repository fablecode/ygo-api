using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class TipSection
    {
        public TipSection()
        {
            Tip = new HashSet<Tip>();
        }

        public long Id { get; set; }
        public long CardId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Card Card { get; set; }
        public ICollection<Tip> Tip { get; set; }
    }
}