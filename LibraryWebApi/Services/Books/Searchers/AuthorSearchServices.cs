using Library.Database;
using Library.Database.Entities.Books;
using LibraryWebApi.Models.Exceptions;
using System.Linq;

namespace LibraryWebApi.Services.Books.Searchers
{
    public class AuthorSearchServices : IAuthorSearchServices
    {
        private DatabaseContext _databaseContext;

        public AuthorSearchServices(DatabaseContext database)
        {
            _databaseContext = database;
        }

        public AuthorEntity GetAuthorByID(int id, bool expected = false)
        {
            var author = _databaseContext.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
                throw new NotFoundException($"Author with id {id} not found.");
            return author;
        }

        public AuthorEntity GetAuthorByName(string lastName, string firstName)
        {
            var author = _databaseContext.Authors.FirstOrDefault(x => x.LastName.ToLower() == lastName.ToLower() && x.FirstName.ToLower() == firstName.ToLower());
            return author;
        }
    }
}