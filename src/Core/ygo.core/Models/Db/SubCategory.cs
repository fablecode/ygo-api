using System;
using System.Collections.Generic;

namespace ygo.infrastructure.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            CardSubCategory = new HashSet<CardSubCategory>();
        }

        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Category Category { get; set; }
        public ICollection<CardSubCategory> CardSubCategory { get; set; }
    }
}
