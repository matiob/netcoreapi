using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Services;
using Library.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<AutorDTO>>>> GetAll()
        {
            try
            {
                var autores = await _autorService.GetAllAsync();
                if (autores.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(autores, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<AutorDTO>>(null, HttpStatusCode.NotFound, "No se encontraron autores");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<AutorDTO>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AutorDTO>>> GetById(string id)
        {
            try
            {
                var autorId = Guid.Parse(id);
                var autor = await _autorService.GetByIdAsync(autorId);

                if (autor != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(autor, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.NotFound, "Autor no encontrado");
                    return NotFound(response);
                }
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<AutorDTO>>> GetByName(string name)
        {
            try
            {
                var autor = await _autorService.GetByNameAsync(name);
                if (autor != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(autor, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.NotFound, "Autor no encontrado");
                    return NotFound(response);
                }
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.NotFound, "Autor no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AutorDTO>>> Create([FromBody] CreateAutorDTO autorPost)
        {
            try
            {
                var autorCreado = await _autorService.CreateAsync(autorPost);
                var response = ApiResponseWrapper.CreateResponse(autorCreado, HttpStatusCode.Created);
                return CreatedAtAction(nameof(GetById), new { id = autorCreado.Id }, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AutorDTO>>> Update(string id, [FromBody] UpdateAutorDTO autorPut)
        {
            var autorId = Guid.Parse(autorPut.Id);
            var paramId = Guid.Parse(id);
            if (paramId != autorId)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }

            try
            {
                var autorEditado = await _autorService.UpdateAsync(autorPut);
                var response = ApiResponseWrapper.CreateResponse(autorEditado);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.NotFound, "Autor no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<AutorDTO>>> Delete(string id)
        {
            try
            {
                var autorId = Guid.Parse(id);
                var autor = await _autorService.DeleteAsync(autorId);
                var response = ApiResponseWrapper.CreateResponse(autor);
                return Ok(response);
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.NotFound, "Autor no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<AutorDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
