using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Models.Magazines;
using LibraryWebApi.Services.Magazines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryWebApi.Controllers.Magazines
{
    [ApiController]
    [Route("api/magazine")]
    public class MagazineController : Controller
    {
        private IMagazineServices _magazineServices;

        #region Constructor

        public MagazineController(IMagazineServices magazineServices)
        {
            _magazineServices = magazineServices;
        }

        #endregion Constructor

        #region Read

        [HttpGet]
        public IActionResult GetAllMagazines()
        {
            try
            {
                var magazineList = _magazineServices.GetAllMagazines();
                return Ok(magazineList);
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
        public IActionResult GetByID(int id)
        {
            try
            {
                var magazine = _magazineServices.GetMagazineById(id);
                return Ok(magazine);
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

        #endregion Read

        #region Create

        [HttpPost]
        public IActionResult CreateMagazine([FromBody] MagazineModel magazineModel)
        {
            try
            {
                _magazineServices.CreateMagazine(magazineModel);
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

        #region Delete

        [HttpDelete("{id}")]
        public IActionResult DeleteMagazine(int id)
        {
            try
            {
                _magazineServices.DeleteMagazine(id);

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

        #region Edit

        [HttpPatch("{id}")]
        public IActionResult EditMagazine(int id, [FromBody] MagazineModel magazineModel)
        {
            try
            {
                _magazineServices.EditMagazine(id, magazineModel);
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
    }
}