using System;

namespace ygo.core.Models.Db
{
    public class Ruling
    {
        public long Id { get; set; }
        public long RulingSectionId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public RulingSection RulingSection { get; set; }
    }
}