using LibraryWebApi.Models.Books;
using System.Collections.Generic;

namespace LibraryWebApi.Services.Books
{
    public interface IAuthorServices
    {
        List<AuthorModel> GetAllAuthors();

        void CreateAuthors(AuthorModel authorModel);

        void DeleteAuthor(int id);

        void EditAuthor(int id, AuthorModel authorModel);

        AuthorModel GetAuthorByID(int id);
    }
}