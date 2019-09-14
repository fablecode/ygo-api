using System;
using System.Collections.Generic;

namespace ygo.application.Paging
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> source, int totalItems, int pageIndex, int pageSize)
        {
            TotalItems = totalItems;
            PageIndex = pageIndex;
            PageSize = pageSize;
            List = source;
        }

        public int TotalItems { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public IEnumerable<T> List { get; }

        public int TotalPages => (int) Math.Ceiling(TotalItems / (double) PageSize);

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public int NextPageNumber =>
            HasNextPage ? PageIndex + 1 : TotalPages;

        public int PreviousPageNumber => HasPreviousPage ? PageIndex - 1 : 1;

        public PagingHeader GetHeader()
        {
            return new PagingHeader(TotalItems, PageIndex, PageSize, TotalPages);
        }
    }
}