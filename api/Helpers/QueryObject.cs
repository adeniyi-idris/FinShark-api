using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;

        public string? CompanyName { get; set; } = null;
        
        public string? SortBy { get; set; } = null;

        //sorting
        public bool IsDescending {get; set;} = false;

        //pagination
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}