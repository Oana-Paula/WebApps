using LibraryWebApi.Models.Books;
using System.Collections.Generic;

namespace LibraryWebApi.Services.Books
{
    public interface IBookServices
    {
        BookModel GetBookById(int id);

        void DeleteBook(int id);

        void Create(BookModel bookModel);

        void EditBook(int id, BookModel bookModel);

        void AddAuthorToBook(int id, int authorid);

        public void RemoveAuthorFromBook(int bookId, int auhtorId);

        List<BookModel> GetAllBooks();
    }
}