using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Category;
using bookStore.Models;

namespace bookStore.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Title = categoryModel.Title,
                Description = categoryModel.Description,
                Books = categoryModel.Books.Select(c => c.ToBookDto()).ToList()
            };
        }

        public static Category ToCategoryFromCreateDto(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Title = categoryDto.Title,
                Description = categoryDto.Description
            };
        }

        public static Category ToCategoryFromUpdateDto(this UpdateCategoryDto categoryDto)
        {
            return new Category
            {
                Title = categoryDto.Title,
                Description = categoryDto.Description
            };
        }

    }
}