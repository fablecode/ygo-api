using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class Banlist
    {
        public Banlist()
        {
            BanlistCard = new HashSet<BanlistCard>();
        }

        public long Id { get; set; }
        public long FormatId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Format Format { get; set; }
        public ICollection<BanlistCard> BanlistCard { get; set; }
    }
}