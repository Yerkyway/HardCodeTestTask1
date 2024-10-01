using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Data;
using OrderServiceApp.Dtos;
using OrderServiceApp.Mappers;

namespace OrderServiceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public BookController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await  _context.Books.ToListAsync();
        var bookDto = books.Select(s => s.ToBookDto());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book.ToBookDto());
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
    {
        var bookModel = bookDto.ToBookFromCreateDto();
        await _context.Books.AddAsync(bookModel);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetBookById), new { id = bookModel.Id }, bookModel.ToBookDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto bookDto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x=>x.Id == id);
        if (book==null)
        {
            return NotFound();
        }
        book.BookName = bookDto.BookName;
        book.Author = bookDto.Author;
        book.Publisher=bookDto.Publisher;
        book.Price=bookDto.Price;
        book.YearOfPublication=bookDto.YearOfPublication;

        await _context.SaveChangesAsync();
        return Ok(book.ToBookDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null) 
        {
            return NotFound();
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}