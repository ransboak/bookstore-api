using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore.Dtos.Book;
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

        public Task<Book?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}