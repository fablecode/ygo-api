using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ygo.application.Paging
{
    public class PagingHeader
    {
        public PagingHeader(
            int totalItems, int pageNumber, int pageSize, int totalPages)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this,
                new JsonSerializerSettings
                {
                    ContractResolver = new
                        CamelCasePropertyNamesContractResolver()
                });
        }
    }
}