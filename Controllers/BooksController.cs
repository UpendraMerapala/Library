using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibearayManagementSystem.Models;

namespace LibearayManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibDBContext _context;
        private object db;

        public BooksController(LibDBContext context)
        {
            _context = context;
        }

        // GET: api/Books


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookdto>>> GetBooks(int page = 1, int pageSize = 10)
        {
           
            if (_context.Books == null)
            {
                return NotFound();
            }
            var books = await _context.Books
        .Select(b => new Bookdto
        {
            BookId = b.BookId,
            BookName = b.BookName,
            PublishedOn = b.PublishedOn,
            Language = b.Language,
            Genre = b.Genre,
            FirstName = b.Authors.FirstName,
            LastName = b.Authors.LastName,
            // Map other properties from the 'Book' entity to 'BookDto'
        })
        .ToListAsync();

            var totalRecords = books.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new

            {

                TotalRecords = totalRecords,

                TotalPages = totalPages,

                Page = page,

                PageSize = pageSize,

                Data = pagedData

            });
        }
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookdto>> GetBook(int id)
        {

            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books
        .Where(b => b.BookId == id)
        .Select(b => new Bookdto
        {
            BookId = b.BookId,
            BookName = b.BookName,
            PublishedOn = b.PublishedOn,
            Language = b.Language,
            Genre = b.Genre,
            FirstName = b.Authors.FirstName,
            LastName = b.Authors.LastName,

            // Map other properties from the 'Book' entity to 'BookDto'
        })
        .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Bookdto bookdto)
        {
            if (id != bookdto.BookId)
            {
                return BadRequest();
            }

            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            // Update the properties of the existing book with the new values
            existingBook.BookId = bookdto.BookId;
            existingBook.BookName = bookdto.BookName;
            existingBook.PublishedOn = bookdto.PublishedOn;
            existingBook.Language = bookdto.Language;
            existingBook.Genre = bookdto.Genre;


            await _context.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Books

        [HttpPost]

        [Route("Create")]

        public IActionResult AddBook([FromBody] BookInputModel bookobj)

        {

            if (bookobj == null)

            {

                return BadRequest();

            }

            if (ModelState.IsValid)

            {

                Book book = new Book();

                book.BookName = bookobj.BookName;

                book.PublishedOn = bookobj.PublishedOn;

                book.Language = bookobj.Language;

                book.Genre = bookobj.Genre;

                book.PublicationId = bookobj.PublicationId;

                book.AuthorId = bookobj.AuthorId;
               

                _context.Books.Add(book);

                _context.SaveChanges();

                return Ok("Book Added Successfully");

            }

            return BadRequest("Book Not Added");

        }

        public class BookInputModel

        {

            public string BookName { get; set; }

            public string Language { get; set; }

            public string Genre { get; set; }

            public DateTime PublishedOn { get; set; }

            public int PublicationId { get; set; }

            public int AuthorId { get; set; }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet]

        [Route("GetBook/{name}")]

        public IActionResult GetBookByName(string name)

        {

            var booksearch = _context.Books.SingleOrDefault(x => x.BookName == name);

            if (booksearch == null)

            {

                return NotFound($"Book with name '{name}' not found.");

            }

            var query = from book in _context.Books
                        where book.BookName.Equals(booksearch.BookName)
                        join authors in _context.authors on book.AuthorId equals authors.AuthorId
                        join publisher in _context.publications on book.PublicationId equals publisher.PublicationId

                        select new

                        {

                            book.BookId,

                            book.BookName,

                            book.PublishedOn,

                            book.Language,

                            book.Genre,

                            authors.FirstName,

                            authors.LastName,

                            publisher.PublishingCompanyName

                        };

            return Ok(query);

        }
    }
}
