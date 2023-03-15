using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRISBlazorServerApp.Models
{
    
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int ItemCount { get; set; }
        public bool HasPreviousPage { get; set; } 
        public bool HasNextPage { get; set; }
    }
}
