using LibraryWebApi.Models.DVD;
using LibraryWebApi.Models.Exceptions;
using LibraryWebApi.Services.DVDs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryWebApi.Controllers.DVDS
{
    [ApiController]
    [Route("api/dvd")]
    public class DVDController : Controller
    {
        private IDVDServies _DVDServices;

        #region Constructor

        public DVDController(IDVDServies dVDServies)
        {
            _DVDServices = dVDServies;
        }

        #endregion Constructor

        #region Read

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            try
            {
                var dvd = _DVDServices.GetDVDById(id);
                return Ok(dvd);
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

        [HttpGet]
        public IActionResult GetAllDVDs()
        {
            try
            {
                var allDVDs = _DVDServices.GetAllDVDs();
                return Ok(allDVDs);
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
        public IActionResult Create([FromBody] DVDModel dvd)
        {
            try
            {
                _DVDServices.CreateDVD(dvd);
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
        public IActionResult EditPatch(int id, [FromBody] DVDModel dvd)
        {
            try
            {
                _DVDServices.EditDVD(id, dvd);
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
        public IActionResult DeleteDVD(int id)
        {
            try
            {
                _DVDServices.DeleteDVD(id);

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