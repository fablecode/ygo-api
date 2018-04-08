using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class CardSubCategory
    {
        public long SubCategoryId { get; set; }
        public long CardId { get; set; }

        public Card Card { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
