using OrderServiceApp.Models;

namespace OrderServiceApp.Mappers;

public static class BookMapper
{
    public static Book ToBookDto(this Book bookModel)
    {
        return new Book
        {
            Id = bookModel.Id,
            BookName = bookModel.BookName,
            Author = bookModel.Author,
            Publisher = bookModel.Publisher,
            Price = bookModel.Price,
            YearOfPublication = bookModel.YearOfPublication,
        };
    }
}