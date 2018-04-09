using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class Limit
    {
        public Limit()
        {
            BanlistCard = new HashSet<BanlistCard>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<BanlistCard> BanlistCard { get; set; }
    }
}