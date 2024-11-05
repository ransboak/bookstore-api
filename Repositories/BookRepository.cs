using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore.Dtos.Book;
using bookStore.Interfaces;
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
        public Task<Book> CreateAsync(CreateBookDto bookDto)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}