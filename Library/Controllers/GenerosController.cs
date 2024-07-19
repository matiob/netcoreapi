using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<GeneroDTO>>>> GetAll()
        {
            try
            {
                var generos = await _generoService.GetAllAsync();
                if (generos.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(generos, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<GeneroDTO>>(null, HttpStatusCode.NotFound, "No se encontraron generos");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<GeneroDTO>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GeneroDTO>>> GetById(string id)
        {
            try
            {
                var generoId = int.Parse(id);
                var genero = await _generoService.GetByIdAsync(generoId);

                if (genero != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(genero, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.NotFound, "Genero no encontrado");
                    return NotFound(response);
                }
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ApiResponse<GeneroDTO>>> GetByName(string name)
        {
            try
            {
                var genero = await _generoService.GetByNameAsync(name);
                if (genero != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(genero, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.NotFound, "Genero no encontrado");
                    return NotFound(response);
                }
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.NotFound, "Genero no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<GeneroDTO>>> Create([FromBody] CreateGeneroDTO generoPost)
        {
            try
            {
                var generoCreado = await _generoService.CreateAsync(generoPost);
                var response = ApiResponseWrapper.CreateResponse(generoCreado, HttpStatusCode.Created);
                return CreatedAtAction(nameof(GetById), new { id = generoCreado.Id }, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GeneroDTO>>> Update(string id, [FromBody] UpdateGeneroDTO generoPut)
        {
            var generoId = int.Parse(generoPut.Id);
            var paramId = int.Parse(id);
            if (paramId != generoId)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }

            try
            {
                var generoEditado = await _generoService.UpdateAsync(generoPut);
                var response = ApiResponseWrapper.CreateResponse(generoEditado);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.NotFound, "Genero no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<GeneroDTO>>> Delete(string id)
        {
            try
            {
                var generoId = int.Parse(id);
                var genero = await _generoService.DeleteAsync(generoId);
                var response = ApiResponseWrapper.CreateResponse(genero);
                return Ok(response);
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.NotFound, "Genero no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<GeneroDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
