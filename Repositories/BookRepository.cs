using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore.Dtos.Book;
using bookStore.Helpers;
using bookStore.Interfaces;
using bookStore.Mappers;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBContext _context;

        public BookRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateAsync(CreateBookDto bookDto)
        {
            var bookModel = bookDto.ToBookFromCreateDto();
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();

            return bookModel;

        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if(book == null){
                return null;
            }

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _context.Books.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);

            if(book == null){
                return null;
            }

            return book;
        }

        public async Task<List<Book>> GetByQueryAsync(BookQueryObject query)
        {
            var books =  _context.Books.Include(c => c.Comments).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Title)){
                books = books.Where(s => s.Title.Contains(query.Title));
            }

            if(query.isDescending){
                books = books.OrderByDescending(s => s.Title);
            
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await books.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if(existingBook == null){
                return null;
            }
            var bookModel = bookDto.ToBookFromUpdateDto();

            existingBook.Title = !string.IsNullOrWhiteSpace(bookModel.Title) ? bookModel.Title : existingBook.Title;
            existingBook.Author = !string.IsNullOrWhiteSpace(bookModel.Author) ? bookModel.Author : existingBook.Author;
            existingBook.CategoryId = bookModel.CategoryId.HasValue ? bookModel.CategoryId.Value : existingBook.CategoryId;
            // existingBook.DatePublished = !string.IsNullOrWhiteSpace(bookModel.DatePublished) ? bookModel.DatePublished : bookModel.DatePublished;

            // existingBook.Title = bookModel.Title;
            // existingBook.Author = bookModel.Author;
            // existingBook.CategoryId = bookModel.CategoryId;
            existingBook.DatePublished = bookModel.DatePublished;

            await _context.SaveChangesAsync();

            return existingBook;
        }
    }
}