using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Category;
using bookStore.Models;

namespace bookStore.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(CreateCategoryDto categoryDto);
        Task<Category?> UpdateAsync(int id, UpdateCategoryDto categoryDto);
        Task<Category?> DeleteAsync(int id);
    }
}