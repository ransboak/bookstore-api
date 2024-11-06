using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using bookStore.Dtos.Category;
using bookStore.Interfaces;
using bookStore.Mappers;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(CreateCategoryDto categoryDto)
        {
            var categoryModel = categoryDto.ToCategoryFromCreateDto();

            await _context.Categories.AddAsync(categoryModel);

            await _context.SaveChangesAsync();

            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return null;

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Books).ToListAsync();

        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return null;

            return category;
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryDto categoryDto)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            var categoryModel = categoryDto.ToCategoryFromUpdateDto();

            if (existingCategory == null) return null;

            existingCategory.Title = !string.IsNullOrWhiteSpace(categoryModel.Title) ? categoryModel.Title : existingCategory.Title;
            existingCategory.Description = !string.IsNullOrWhiteSpace(categoryModel.Description) ? categoryModel.Description : existingCategory.Description;

            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}