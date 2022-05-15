using Library.Database;
using Library.Database.Entities.Books;
using LibraryWebApi.Models.Books;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.Books.Searchers;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Services.Books
{
    public class AuthorServices : IAuthorServices
    {
        private DatabaseContext _databaseContext;
        private IAuthorSearchServices _authorSearchServices;

        public AuthorServices(DatabaseContext database, IAuthorSearchServices authorSearchServices)
        {
            _databaseContext = database;
            _authorSearchServices = authorSearchServices;
        }

        public List<AuthorModel> GetAllAuthors()
        {
            List<AuthorModel> authorsList = new List<AuthorModel>();
            var authorsEntities = _databaseContext.Authors;
            if (authorsEntities.Count() == 0)
            {
                throw new NotFoundException("No authorsList available");
            }
            authorsList.AddRange(authorsEntities.Select(value => new AuthorModel()
            {
                Id = value.Id,
                LastName = value.LastName,
                FirstName = value.FirstName
            }));
            return authorsList;
        }

        public AuthorModel GetAuthorByID(int id)
        {
            AuthorModel authorModel = new AuthorModel();
            AuthorEntity authorEntity = _authorSearchServices.GetAuthorByID(id, true);
            authorEntity.FirstName = authorModel.FirstName;
            authorEntity.LastName = authorModel.LastName;
            return authorModel;
        }

        public void CreateAuthors(AuthorModel authorModel)
        {
            if (authorModel.Id != null)
            {
                throw new BadRequestException("Id can not be added. Is primary key and will be added automatic on create");
            }
            var authorExists = _authorSearchServices.GetAuthorByName(authorModel.LastName, authorModel.FirstName);
            if (authorExists != null)
            {
                throw new BadRequestException("Author is already in database");
            }
            AuthorEntity authorEntity = new()
            {
                LastName = authorModel.LastName,
                FirstName = authorModel.FirstName
            };

            _databaseContext.Add(authorEntity);
            _databaseContext.SaveChanges();
        }

        public void EditAuthor(int id, AuthorModel authorModel)
        {
            if (authorModel.Id != null)
            {
                throw new BadRequestException("Id can not be edit");
            }
            var authorEntity = _authorSearchServices.GetAuthorByID(id, true);
            if (authorModel.LastName != null)
            {
                authorEntity.LastName = authorModel.LastName;
            }
            if (authorModel.FirstName != null)
            {
                authorEntity.FirstName = authorModel.FirstName;
            }
            _databaseContext.Update(authorEntity);
            _databaseContext.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            var authorEntity = _authorSearchServices.GetAuthorByID(id, true);
            var bookAuthorMapping = _databaseContext.BookAuthorMap.Where(x => x.Author == authorEntity);

            if (bookAuthorMapping.Count() > 0)
                _databaseContext.BookAuthorMap.RemoveRange(bookAuthorMapping);

            _databaseContext.Remove(authorEntity);
            _databaseContext.SaveChanges();
        }
    }
}