using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManyToMany.Models;

namespace ManyToMany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _context;

        public BooksController(BookDbContext context)
        {
            _context = context;
        }

        // A simple DTO to return nice, flattened results
        public record BookAuthorView(
            int BookId, string BookTitle,
            int AuthorId, string AuthorName,
            int PublicationYear, decimal Price
        );

        // GET: api/BookAuthors
        // List all links (projected with book/author info)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookAuthorView>>> GetAll()
        {
            var data = await _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .OrderBy(ba => ba.Book!.Title)
                .Select(ba => new BookAuthorView(
                    ba.BookId, ba.Book!.Title!, ba.AuthorId, ba.Author!.Name!,
                    ba.Book!.PublicationYear, ba.Book!.Price))
                .ToListAsync();

            return data;
        }
        // GET: api/BookAuthors/1/2  (composite key)
        [HttpGet("{bookId:int}/{authorId:int}")]
        public async Task<ActionResult<BookAuthorView>> GetOne(int bookId, int authorId)
        {
            var link = await _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .Where(ba => ba.BookId == bookId && ba.AuthorId == authorId)
                .Select(ba => new BookAuthorView(
                    ba.BookId, ba.Book!.Title!, ba.AuthorId, ba.Author!.Name!,
                    ba.Book!.PublicationYear, ba.Book!.Price))
                .SingleOrDefaultAsync();

            return link is null ? NotFound() : link;
        }

        //// PUT: api/Books/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/BookAuthors
        // Body: { "bookId": 1, "authorId": 2 }
        [HttpPost]
        public async Task<ActionResult<BookAuthorView>> LinkAuthorToBook(BookAuthor dto)
        {
            // guard: exists?
            var exists = await _context.BookAuthors
                .AnyAsync(ba => ba.BookId == dto.BookId && ba.AuthorId == dto.AuthorId);

            if (exists) return Conflict("This author is already linked to the book.");

            _context.BookAuthors.Add(dto);
            await _context.SaveChangesAsync();

            // return created link
            return await GetOne(dto.BookId, dto.AuthorId);
        }

        // DELETE: api/BookAuthors/1/2
        [HttpDelete("{bookId:int}/{authorId:int}")]
        public async Task<IActionResult> UnlinkAuthorFromBook(int bookId, int authorId)
        {
            var link = await _context.BookAuthors
                .FirstOrDefaultAsync(ba => ba.BookId == bookId && ba.AuthorId == authorId);

            if (link is null) return NotFound();

            _context.BookAuthors.Remove(link);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---- SEARCH ----
        // GET: api/BookAuthors/search?q=core&author=Hadiya&yearFrom=2020&maxPrice=35
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookAuthorView>>> Search(
          
            [FromQuery] string? title,
            [FromQuery] string? author,
            [FromQuery] int? yearFrom,
            [FromQuery] int? yearTo,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var query = _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(title))
            {
                var pat = $"%{title}%";
                query = query.Where(ba => EF.Functions.Like(ba.Book!.Title!, pat));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                var pat = $"%{author}%";
                query = query.Where(ba => EF.Functions.Like(ba.Author!.Name!, pat));
            }

            if (yearFrom.HasValue) query = query.Where(ba => ba.Book!.PublicationYear >= yearFrom.Value);
            if (yearTo.HasValue) query = query.Where(ba => ba.Book!.PublicationYear <= yearTo.Value);
            if (minPrice.HasValue) query = query.Where(ba => ba.Book!.Price >= minPrice.Value);
            if (maxPrice.HasValue) query = query.Where(ba => ba.Book!.Price <= maxPrice.Value);

            var results = await query
                .OrderBy(ba => ba.Book!.Title)
                .Select(ba => new BookAuthorView(
                    ba.BookId, ba.Book!.Title!, ba.AuthorId, ba.Author!.Name!,
                    ba.Book!.PublicationYear, ba.Book!.Price))
                .ToListAsync();

            return results;
        }

        // Convenience: /api/BookAuthors/by-book/1/authors
        [HttpGet("by-book/{bookId:int}/authors")]
        public async Task<ActionResult<IEnumerable<object>>> GetAuthorsForBook(int bookId)
        {
            var authors = await _context.BookAuthors
                .Where(ba => ba.BookId == bookId)
                .Select(ba => new { ba.AuthorId, ba.Author!.Name })
                .ToListAsync();

            return authors;
        }

        // Convenience: /api/BookAuthors/by-author/2/books
        [HttpGet("by-author/{authorId:int}/books")]
        public async Task<ActionResult<IEnumerable<object>>> GetBooksForAuthor(int authorId)
        {
            var books = await _context.BookAuthors
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => new { ba.BookId, ba.Book!.Title, ba.Book!.PublicationYear, ba.Book!.Price })
                .ToListAsync();

            return books;
        }

    }
}
