using System;

namespace ygo.core.Models.Db
{
    public class Tip
    {
        public long Id { get; set; }
        public long TipSectionId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public TipSection TipSection { get; set; }
    }
}