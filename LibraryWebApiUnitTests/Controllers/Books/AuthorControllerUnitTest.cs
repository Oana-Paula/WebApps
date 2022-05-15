using FakeItEasy;
using LibraryWebApi.Controllers.Books;
using LibraryWebApi.Models.Books;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryWebApiUnitTests.Controllers
{
    public class AuthorControllerUnitTest
    {
        #region GetAuthorByID

        [Fact]
        public void TestGetAuthorById_WithOkResult()
        {
            //Arrange
            AuthorModel authorModel = new AuthorModel()
            {
                FirstName = "William",
                LastName = "Shakespeare"
            };
            int count = 5;
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAuthorByID(count)).Returns(authorModel);
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAuthorById(count);
            //Assert
            var result = actionResult as OkObjectResult;
            var authorResult = result.Value as AuthorModel;
            Assert.Equal(authorModel, authorResult);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void TestGetAuthorById_WithNotFound()
        {
            int count = 5;
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAuthorByID(count)).Throws(new NotFoundException());
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAuthorById(count);
            //Assert
            var result = (IStatusCodeActionResult)actionResult;
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void TestGetAuthorById_WithInternalError()
        {
            int count = 5;
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAuthorByID(count)).Throws(new Exception());
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAuthorById(count);
            //Assert
            var result = (IStatusCodeActionResult)actionResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        #endregion GetAuthorByID

        #region GetAllAuthors

        [Fact]
        public void TestGetAllAuthors_ResultOK()
        {
            List<AuthorModel> authorsList = new List<AuthorModel>()
            {
                new AuthorModel()
                {
                    Id = 1,
                    FirstName = "William",
                    LastName ="Shakespeare"
                },
                new AuthorModel()
                {
                    Id = 2,
                    FirstName = "Jules",
                    LastName ="Verne"
                },
            };
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAllAuthors()).Returns(authorsList);
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAllAuthors();
            //Assert
            var result = actionResult as OkObjectResult;
            var authorsLisResultt = result.Value as List<AuthorModel>;
            Assert.Equal(authorsList, authorsLisResultt);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void TestGetAllAuthors_WithNotFound()
        {
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAllAuthors()).Throws(new NotFoundException("No author available"));
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAllAuthors();

            //Assert
            var objectResult = actionResult as NotFoundObjectResult;

            var resultStatus = (IStatusCodeActionResult)actionResult;
            Assert.Equal("No author available", objectResult.Value.ToString());
            Assert.Equal(StatusCodes.Status404NotFound, resultStatus.StatusCode);
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public void TestGetAllAuthors_WithInternalError()
        {
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.GetAllAuthors()).Throws(new Exception());
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.GetAllAuthors();

            //Assert
            var objectResult = actionResult as NotFoundObjectResult;

            var resultStatus = (IStatusCodeActionResult)actionResult;
            var result = (IStatusCodeActionResult)actionResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        #endregion GetAllAuthors

        #region CreateAuthor

        [Fact]
        public void TestCreateAuthor_ResultOK()
        {
            var authorModel = new AuthorModel()
            {
                Id = 2,
                FirstName = "Jules",
                LastName = "Verne"
            };
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.CreateAuthors(authorModel)).DoesNothing();
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.CreateAuthors(authorModel);
            //Assert
            var resultStatus = (IStatusCodeActionResult)actionResult;
            Assert.Equal(StatusCodes.Status200OK, resultStatus.StatusCode);
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public void TestCreateAuthor_WithBadRequest()
        {
            var authorModel = new AuthorModel()
            {
                Id = 2,
                FirstName = "Jules",
                LastName = "Verne"
            };
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.CreateAuthors(authorModel)).Throws(new BadRequestException("Author is already in database"));
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.CreateAuthors(authorModel);
            //Assert
            var objectResult = actionResult as BadRequestObjectResult;

            var resultStatus = (IStatusCodeActionResult)actionResult;
            Assert.Equal("Author is already in database", objectResult.Value.ToString());
            Assert.Equal(StatusCodes.Status400BadRequest, resultStatus.StatusCode);
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public void TestCreateAuthor_WithInternalError()
        {
            var authorModel = new AuthorModel()
            {
                Id = 2,
                FirstName = "Jules",
                LastName = "Verne"
            };
            var fakeAuthorServices = A.Fake<IAuthorServices>();
            A.CallTo(() => fakeAuthorServices.CreateAuthors(authorModel)).Throws(new Exception());
            var controller = new AuthorController(fakeAuthorServices);
            //Act
            var actionResult = controller.CreateAuthors(authorModel);

            //Assert
            var objectResult = actionResult as NotFoundObjectResult;

            var resultStatus = (IStatusCodeActionResult)actionResult;
            var result = (IStatusCodeActionResult)actionResult;
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        #endregion CreateAuthor
    }
}