using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Data;
using OrderServiceApp.Dtos;
using OrderServiceApp.Models;

namespace OrderServiceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;

    public BookController(ApplicationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await  _context.Books.ToListAsync();
        var bookDto = books.Select(s => _mapper.Map<BookDto>(s));
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

        return Ok(_mapper.Map<BookDto>(book));
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookRequestDto bookDto)
    {
        var bookModel = _mapper.Map<Book>(bookDto);
        await _context.Books.AddAsync(bookModel);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetBookById), new { id = bookModel.Id }, _mapper.Map<BookDto>(bookModel));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookDto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x=>x.Id == id);
        if (book==null)
        {
            return NotFound();
        }
        
        _mapper.Map(updateBookDto, book);
        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<BookDto>(book));
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