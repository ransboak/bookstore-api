using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models
{
    [Table("Books")]
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateOnly DatePublished { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category {get; set;}

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}