using Microsoft.AspNetCore.Mvc;
using OrderServiceApp.Data;
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
    
    
}