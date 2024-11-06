using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Category;
using bookStore.Interfaces;
using bookStore.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepo.GetAllAsync();

            var categoryDto = categories.Select(c => c.ToCategoryDto());

            return Ok(categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category.ToCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
        {
            var categoryModel = await _categoryRepo.CreateAsync(categoryDto);

            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var categoryModel = await _categoryRepo.UpdateAsync(id, categoryDto);

            if (categoryModel == null) return NotFound();

            return Ok(categoryModel.ToCategoryDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var category = await _categoryRepo.DeleteAsync(id);

            if (category == null) return NotFound("Category does not exist");

            return NoContent();
        }
    }
}