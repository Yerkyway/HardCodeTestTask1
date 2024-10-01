using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAllBooks()
    {
        var books = _context.Books.ToList()
            .Select(s => s.ToBookDto());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById([FromRoute] int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book.ToBookDto());
    }


    [HttpPost]
    public IActionResult Create([FromBody] CreateBookRequestDto bookDto)
    {
        var bookModel = bookDto.ToBookFromCreateDto();
        _context.Books.Add(bookModel);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(GetBookById), new { id = bookModel.Id }, bookModel.ToBookDto());
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateBookRequestDto bookDto)
    {
        var book = _context.Books.FirstOrDefault(x=>x.Id == id);
        if (book==null)
        {
            return NotFound();
        }
        book.BookName = bookDto.BookName;
        book.Author = bookDto.Author;
        book.Publisher=bookDto.Publisher;
        book.Price=bookDto.Price;
        book.YearOfPublication=bookDto.YearOfPublication;

        _context.SaveChanges();
        return Ok(book.ToBookDto());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);
        if (book == null) 
        {
            return NotFound();
        }
        _context.Books.Remove(book);
        _context.SaveChanges();
        
        return NoContent();
    }
}