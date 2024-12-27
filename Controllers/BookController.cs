using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagerAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // In-memory list to store books
        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "1234567890", PublicationDate = new DateTime(2020, 1, 1) },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "0987654321", PublicationDate = new DateTime(2021, 6, 15) }
        };

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(Books);
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }
            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest(new { Message = "Invalid book data" });
            }

            // Generate a new ID for the book
            book.Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
            Books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/Books/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.ISBN = updatedBook.ISBN;
            book.PublicationDate = updatedBook.PublicationDate;

            return NoContent();
        }

        // DELETE: api/Books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            Books.Remove(book);
            return NoContent();
        }
    }
}
