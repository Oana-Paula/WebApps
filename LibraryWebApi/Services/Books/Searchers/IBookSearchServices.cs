using Library.Database.Entities.Books;

namespace LibraryWebApi.Services.Books
{
    public interface IBookSearchServices
    {
        BookEntity GetBookByID(int id, bool expected = false);

        BookEntity GetBookByTitle(string title);
    }
}