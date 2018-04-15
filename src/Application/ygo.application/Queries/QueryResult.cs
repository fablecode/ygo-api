using System.Collections.Generic;

namespace ygo.application.Queries
{
    public class QueryResult
    {
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }

        public object Data { get; set; }
    }
}