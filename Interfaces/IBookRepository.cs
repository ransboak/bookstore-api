using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Book;
using bookStore.Helpers;
using bookStore.Models;

namespace bookStore.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetByQueryAsync(BookQueryObject query);
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(CreateBookDto bookDto);
        Task<Book?> UpdateAsync(int id, UpdateBookDto bookDto);
        Task<Book?> DeleteAsync(int id);
    }
}