using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderServiceApp;
using OrderServiceApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("api", new OpenApiInfo{ Title = "OrderServiceApi", Version = "v1" });
});
builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/api/swagger.json", "OrderServiceApi");
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(x=>x.MapControllers());

app.Run();
