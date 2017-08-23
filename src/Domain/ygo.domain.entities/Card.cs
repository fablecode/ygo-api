using System;
using System.Collections.Generic;

namespace ygo.domain.entities
{
    public class Card
    {
        public long Id { get; set; }

        //[StringLength(50)]
        public string CardNumber { get; set; }

        //[Required]
        //[StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int? CardLevel { get; set; }

        public int? CardRank { get; set; }

        public int? Atk { get; set; }

        public int? Def { get; set; }

        public string Tips { get; set; }

        public string Rulings { get; set; }

        public string Trivia { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<BanlistCard> BanlistCards { get; set; }
    }
}