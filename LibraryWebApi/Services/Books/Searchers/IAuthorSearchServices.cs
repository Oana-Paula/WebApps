using Library.Database.Entities.Books;

namespace LibraryWebApi.Services.Books.Searchers
{
    public interface IAuthorSearchServices
    {
        public AuthorEntity GetAuthorByID(int id, bool expected = false);

        AuthorEntity GetAuthorByName(string lastName, string firstName);
    }
}