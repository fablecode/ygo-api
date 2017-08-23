using System;
using System.Collections.Generic;

namespace ygo.domain.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();
    }
}