using Library.Database;
using Library.Database.Entities.Books;
using LibraryWebApi.Models.Exceptions;
using System.Linq;

namespace LibraryWebApi.Services.Books
{
    public class BookSearchServices : IBookSearchServices
    {
        private DatabaseContext _databaseContext;

        public BookSearchServices(DatabaseContext database)
        {
            _databaseContext = database;
        }

        public BookEntity GetBookByID(int id, bool expected = false)
        {
            var book = _databaseContext.Books.FirstOrDefault(x => x.Id == id);
            if (book == null && expected == true)
                throw new NotFoundException($"Book with id {id} not found.");
            return book;
        }

        public BookEntity GetBookByTitle(string title)
        {
            var book = _databaseContext.Books.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
            return book;
        }
    }
}