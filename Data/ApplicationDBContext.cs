using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Models;

namespace OrderServiceApp.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }
}