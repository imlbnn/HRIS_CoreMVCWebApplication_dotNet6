using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonConstructorAttribute = Newtonsoft.Json.JsonConstructorAttribute;

namespace HRISBlazorServerApp.Models
{
    
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int ItemCount { get; }

        //[JsonConstructor]
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            //TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            ItemCount = count;
            Items = items;
        }

        //[JsonConstructor]
        //public PaginatedList(List<T> items, int totalCount, int filteredCount, int pageIndex, int pageSize)
        //{
        //    PageIndex = pageIndex;
        //    TotalPages = (int)Math.Ceiling(filteredCount / (double)pageSize);
        //    TotalCount = totalCount;
        //    ItemCount = filteredCount;
        //    Items = items;
        //}

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }
}
