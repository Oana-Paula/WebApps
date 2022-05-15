using LibraryWebApi.Models.Books;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Library.Controllers.Books
{
    [ApiController]
    [Route("api/book")]
    public class BookController : Controller
    {
        private IBookServices _bookService;

        #region Constructor

        public BookController(IBookServices bookSServices)
        {
            _bookService = bookSServices;
        }

        #endregion Constructor

        #region Read

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var allBooks = _bookService.GetAllBooks();
                return Ok(allBooks);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBookByID(int id)
        {
            try
            {
                var book = _bookService.GetBookById(id);
                return Ok(book);
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Read

        #region Create

        [HttpPost]
        public IActionResult CreateBook([FromBody] BookModel bookModel)
        {
            try
            {
                _bookService.Create(bookModel);
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Create

        #region Edit

        [HttpPatch("{id}")]
        public IActionResult EditBook(int id, [FromBody] BookModel bookModel)
        {
            try
            {
                _bookService.EditBook(id, bookModel);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{bookId}/author/{authorId}")]
        public IActionResult AddAuthorToBook(int bookId, int authorId)
        {
            try
            {
                _bookService.AddAuthorToBook(bookId, authorId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{bookId}/author/{authorId}")]
        public IActionResult RemoveAuthorFromBook(int bookId, int authorId)
        {
            try
            {
                _bookService.RemoveAuthorFromBook(bookId, authorId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (BadRequestException ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Edit

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Delete
    }
}