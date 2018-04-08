using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class BanlistCard
    {
        public long BanlistId { get; set; }
        public long CardId { get; set; }
        public long LimitId { get; set; }

        public Banlist Banlist { get; set; }
        public Card Card { get; set; }
        public Limit Limit { get; set; }
    }
}
