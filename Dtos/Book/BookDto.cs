using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Comment;
using bookStore.Models;

namespace bookStore.Dtos.Book
{
    public class BookDto
    {
         public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateOnly DatePublished { get; set; }

        public int? CategoryId { get; set; }

         public List<CommentDto> Comments { get; set; }
    }
}