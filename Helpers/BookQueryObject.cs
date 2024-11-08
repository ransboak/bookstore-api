using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Helpers
{
    public class BookQueryObject
    {
        public string? Title { get; set; }
        public bool isDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
    }
}