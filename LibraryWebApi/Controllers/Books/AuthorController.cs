using LibraryWebApi.Models.Books;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryWebApi.Controllers.Books
{
    [Route("api/author")]
    public class AuthorController : Controller
    {
        private IAuthorServices _authorServices;

        #region Constructor

        public AuthorController(IAuthorServices authorService)
        {
            _authorServices = authorService;
        }

        #endregion Constructor

        #region Read

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            try
            {
                var allAuthors = _authorServices.GetAllAuthors();
                return Ok(allAuthors);
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
        public IActionResult GetAuthorById(int id)
        {
            try
            {
                var author = _authorServices.GetAuthorByID(id);
                return Ok(author);
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
        public IActionResult CreateAuthors([FromBody] AuthorModel authorModel)
        {
            try
            {
                _authorServices.CreateAuthors(authorModel);
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
        public IActionResult EditAuthor(int id, [FromBody] AuthorModel authorModel)
        {
            try
            {
                _authorServices.EditAuthor(id, authorModel);
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

        #endregion Edit

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult DeletAuthor(int id)
        {
            try
            {
                _authorServices.DeleteAuthor(id);
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