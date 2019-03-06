using System.Collections.Generic;

namespace ygo.core.Models
{
    public class SpellCardModel
    {
        public int? CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> SubCategoryIds { get; set; }
    }
}