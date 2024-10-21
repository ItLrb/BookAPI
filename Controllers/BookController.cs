using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreDB.Models;
using BookStoreDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        public BookController(BookService bookService) => _bookService = bookService;

        [HttpGet]
        public async Task<List<Book>> Get() => await _bookService.GetBook();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id) 
        {
            var book = await _bookService.GetBook(id);
            if (book is null) 
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book newBook) 
        {
            await _bookService.CreateBook(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);  
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updateBook) 
        {
            var book = await _bookService.GetBook(id);
            if (book is null) 
            {
                return NotFound();
            }
            updateBook.Id = book.Id;
            await _bookService.UpdateBook(id, updateBook);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id) 
        {
            var book = await _bookService.GetBook(id);
            if (book is null) 
            {
                return NotFound();
            }
            await _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}