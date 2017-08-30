using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Category
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<SubCategory> SubCategory { get; set; }
    }
}