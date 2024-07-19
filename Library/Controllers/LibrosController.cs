using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Services;
using Library.Validations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _libroService;

        public LibrosController(ILibroService libroService) 
        {
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<LibroDTO>>>> GetAll()
        {
            try
            {
                var libros = await _libroService.GetAllAsync();
                if (libros.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(libros, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<LibroDTO>>(null, HttpStatusCode.NotFound, "No se encontraron libros");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<LibroDTO>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("between")]
        public async Task<ActionResult<ApiResponse<List<LibroDTO>>>> GetBetween([FromQuery]string fechaInicio, [FromQuery] string fechaFin)
        {
            try
            {
                var esFechaInicioValida = ValidationHelper.BeValidDate(fechaInicio);
                var esFechaFinValida = ValidationHelper.BeValidDate(fechaFin);
                if (!esFechaInicioValida || !esFechaFinValida)
                {
                    var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.BadRequest, "Formato de fechas inválido");
                    return BadRequest(response);
                }

                var libros = await _libroService.GetBetweenDatesAsync(fechaInicio, fechaFin);

                if (libros.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(libros, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<LibroDTO>>(null, HttpStatusCode.NotFound, "No se encontraron libros entre las fechas especificadas");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<LibroDTO>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<LibroDTO>>> GetById(string id)
        {
            try
            {
                var libroId = Guid.Parse(id);
                var libro = await _libroService.GetByIdAsync(libroId);

                if (libro != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(libro, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.NotFound, "Libro no encontrado");
                    return NotFound(response);
                }
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<ApiResponse<LibroDTO>>> GetByTitle(string title)
        {
            try
            {
                var libro = await _libroService.GetByTitleAsync(title);
                if (libro != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(libro, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.NotFound, "Libro no encontrado");
                    return NotFound(response);
                }
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.NotFound, "Libro no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<LibroDTO>>> Create([FromBody] CreateLibroDTO libroPost)
        {
            try
            {
                var libroCreado = await _libroService.CreateAsync(libroPost);
                var response = ApiResponseWrapper.CreateResponse(libroCreado, HttpStatusCode.Created);
                return CreatedAtAction(nameof(GetById), new { id = libroCreado.Id }, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<LibroDTO>>> Update(string id, [FromBody] UpdateLibroDTO libroPut)
        {
            var libroId = Guid.Parse(libroPut.Id);
            var paramId = Guid.Parse(id);
            if (paramId != libroId) 
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }

            try
            {
                var libroEditado = await _libroService.UpdateAsync(libroPut);
                var response = ApiResponseWrapper.CreateResponse(libroEditado);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.NotFound, "Libro no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<LibroDTO>>> Delete(string id)
        {
            try
            {
                var libroId = Guid.Parse(id);
                var libro = await _libroService.DeleteAsync(libroId);
                var response = ApiResponseWrapper.CreateResponse(libro);
                return Ok(response);
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.NotFound, "Libro no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<LibroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
