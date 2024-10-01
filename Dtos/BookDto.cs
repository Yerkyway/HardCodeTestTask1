namespace OrderServiceApp.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string BookName { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int YearOfPublication { get; set; }
}