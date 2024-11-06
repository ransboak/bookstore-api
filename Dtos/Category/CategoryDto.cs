using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Book;
using bookStore.Models;

namespace bookStore.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<BookDto> Books { get; set; }
    }
}