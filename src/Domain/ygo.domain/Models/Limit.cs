using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Limit
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<BanlistCard> BanlistCards { get; set; } = new HashSet<BanlistCard>();
    }
}