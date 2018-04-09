using System;
using System.Collections.Generic;

namespace ygo.core.Models.Db
{
    public class Format
    {
        public Format()
        {
            Banlist = new HashSet<Banlist>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<Banlist> Banlist { get; set; }
    }
}