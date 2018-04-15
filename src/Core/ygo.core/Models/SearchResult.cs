using System.Collections.Generic;

namespace ygo.core.Models
{
    public class SearchResult<T> where T : class
    {
        public int TotalRecords { get; set; }

        public List<T> Items { get; set; }
    }
}