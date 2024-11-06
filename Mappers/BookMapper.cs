using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Book;
using bookStore.Models;

namespace bookStore.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book bookModel){
            return new BookDto{
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                DatePublished = bookModel.DatePublished,
                CategoryId = bookModel.CategoryId,
                Comments = bookModel.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }
        public static Book ToBookFromCreateDto(this CreateBookDto bookDto){
            return new Book{
                Title = bookDto.Title,
                Author = bookDto.Author,
                DatePublished = bookDto.DatePublished,
                CategoryId = bookDto.CategoryId,
            };
        }
        public static Book ToBookFromUpdateDto(this UpdateBookDto bookDto){
            return new Book{
                Title = bookDto.Title,
                Author = bookDto.Author,
                DatePublished = bookDto.DatePublished,
                CategoryId = bookDto.CategoryId,
            };
        }
    }
}