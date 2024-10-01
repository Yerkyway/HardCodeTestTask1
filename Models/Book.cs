using System.ComponentModel.DataAnnotations.Schema;

namespace OrderServiceApp.Models;

public class Book
{
    public int Id { get; set; }
    public string BookName { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    public int YearOfPublication { get; set; }
}