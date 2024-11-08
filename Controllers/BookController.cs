using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Dtos.Book;
using bookStore.Helpers;
using bookStore.Interfaces;
using bookStore.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BookController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepo.GetAllAsync();

            var bookDto = books.Select(s => s.ToBookDto());

            return Ok(bookDto);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetByQuery([FromQuery] BookQueryObject query)
        {
            var books = await _bookRepo.GetByQueryAsync(query);

            var bookDto = books.Select(s => s.ToBookDto());

            return Ok(bookDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null) return NotFound();

            return Ok(book.ToBookDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto bookDto)
        {
            var bookModel = await _bookRepo.CreateAsync(bookDto);

            return CreatedAtAction(nameof(GetById), new { id = bookModel.Id }, bookModel.ToBookDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookDto bookDto)
        {
            var bookModel = await _bookRepo.UpdateAsync(id, bookDto);

            if (bookModel == null) return NotFound();

            return Ok(bookModel.ToBookDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var book = await _bookRepo.DeleteAsync(id);

            if (book == null) return NotFound("Book does not exist");

            return NoContent();
        }
    }
}