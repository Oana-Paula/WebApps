using Library.Database;
using Library.Database.Entities.Books;
using LibraryWebApi.Models.Books;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.Books.Searchers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryWebApi.Services.Books
{
    public class BookServices : IBookServices
    {
        private DatabaseContext _databaseContext;
        private IBookSearchServices _bookSearchService;
        private IAuthorSearchServices _authorSearchServices;

        #region Constructor

        public BookServices(DatabaseContext db, IBookSearchServices bookSearch, IAuthorSearchServices authorSearchServices)
        {
            _databaseContext = db;
            _bookSearchService = bookSearch;
            _authorSearchServices = authorSearchServices;
        }

        #endregion Constructor

        #region Read

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> bookList = new List<BookModel>();
            var bookEntities = _databaseContext.Books;
            if (bookEntities.Count() == 0)
            {
                throw new NotFoundException("No bookModel available");
            }
            bookList.AddRange(bookEntities.Select(value => new BookModel()
            {
                Id = value.Id,
                Title = value.Title,
                Category = value.Category,
                Description = value.Description,
            }));
            return bookList;
        }

        #endregion Read

        #region Create

        public void Create(BookModel bookModel)
        {
            if (bookModel.Id != null)
            {
                throw new BadRequestException("Id can not be added. Is primary key and will be added automatic on create");
            }
            var bookExists = _bookSearchService.GetBookByTitle(bookModel.Title);
            if (bookExists != null)
            {
                throw new BadRequestException("Author is already in database");
            }
            if (_databaseContext.Books.FirstOrDefault(x => x.Title.ToLower() == bookModel.Title.ToLower()) != null)
            {
                return;
            }
            BookEntity newBook = new();
            newBook.Title = bookModel.Title;
            newBook.Category = bookModel.Category;
            newBook.Description = bookModel.Description;
            newBook.Category = bookModel.Category;
            _databaseContext.Books.Add(newBook);
            foreach (AuthorModel authorModel in bookModel.AuthorList)
            {
                var author = _databaseContext.Authors.FirstOrDefault(x => x.FirstName.ToLower() == authorModel.FirstName.ToLower() && x.LastName.ToLower() == authorModel.LastName.ToLower());
                if (author == null)
                {
                    author = new()
                    {
                        FirstName = authorModel.FirstName,
                        LastName = authorModel.LastName
                    };
                    _databaseContext.Authors.Add(author);
                }

                BookAuthorEntity newBookAuthor = new()
                {
                    Book = newBook,
                    Author = author
                };
                _databaseContext.BookAuthorMap.Add(newBookAuthor);
            }
            _databaseContext.SaveChanges();
        }

        #endregion Create

        #region Edit

        public void EditBook(int id, BookModel bookModel)
        {
            if (bookModel.Id != null)
            {
                throw new BadRequestException("Id can not be edit");
            }
            var book = _bookSearchService.GetBookByID(id, true);
            if (bookModel.Title != null)
            {
                book.Title = bookModel.Title;
            }
            if (bookModel.Description != null)
            {
                book.Description = bookModel.Description;
            }
            if (bookModel.Category != null)
            {
                book.Category = bookModel.Category;
            }
            _databaseContext.Update(book);
            _databaseContext.SaveChanges();
        }

        public BookModel GetBookById(int id)
        {
            BookModel bookModel = new BookModel();
            BookEntity bookEntity = _bookSearchService.GetBookByID(id, true);
            bookModel.Title = bookEntity.Title;
            bookModel.Category = bookEntity.Category;
            bookModel.Description = bookModel.Description;
            var bookAuthorMappingList = _databaseContext.BookAuthorMap.Include(db => db.Book).Include(db => db.Author).Where(db => db.Book == bookEntity).ToList();
            bookModel.AuthorList.AddRange(bookAuthorMappingList.Select(value => new AuthorModel() { LastName = value.Author.LastName, FirstName = value.Author.FirstName }));

            return bookModel;
        }

        public void RemoveAuthorFromBook(int bookId, int auhtorId)
        {
            var authorEntity = _authorSearchServices.GetAuthorByID(auhtorId, true);
            var bookEntity = _bookSearchService.GetBookByID(bookId, true);

            var checkMapping = _databaseContext.BookAuthorMap.FirstOrDefault(x => x.Author == authorEntity && x.Book == bookEntity);
            if (checkMapping != null)
            {
                _databaseContext.BookAuthorMap.Remove(checkMapping);
                _databaseContext.SaveChanges();
            }
            else
                throw new BadRequestException();
        }

        public void AddAuthorToBook(int bookId, int auhtorId)
        {
            var bookEntity = _bookSearchService.GetBookByID(bookId, true);
            var authorEntity = _authorSearchServices.GetAuthorByID(auhtorId, true);

            var checkMapping = _databaseContext.BookAuthorMap.FirstOrDefault(x => x.Author == authorEntity && x.Book == bookEntity);
            if (checkMapping == null)
            {
                BookAuthorEntity newBookAuthorMapping = new()
                {
                    Book = bookEntity,
                    Author = authorEntity
                };
                _databaseContext.BookAuthorMap.Add(newBookAuthorMapping);
                _databaseContext.SaveChanges();
            }
            else
            {
                throw new ConflictException("Author already added");
            }
        }

        #endregion Edit

        #region Delete

        public void DeleteBook(int id)
        {
            var bookEntity = _bookSearchService.GetBookByID(id, true);
            var bookAuthorMapping = _databaseContext.BookAuthorMap.Where(x => x.Book == bookEntity);

            if (bookAuthorMapping.Count() > 0)
                _databaseContext.BookAuthorMap.RemoveRange(bookAuthorMapping);

            _databaseContext.Remove(bookEntity);
            _databaseContext.SaveChanges();
        }

        #endregion Delete
    }
}