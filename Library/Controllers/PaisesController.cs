using Library.Domains;
using Library.DTOs.Response;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly IPaisService _paisService;

        public PaisesController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Pais>>> GetAll()
        {
            try
            {
                var paises = await _paisService.GetAllAsync();
                if (paises.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(paises, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<Pais>>(null, HttpStatusCode.NotFound, "No se encontraron paises");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<Pais>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<ApiResponse<Pais>>> GetByCode(string code)
        {
            try
            {
                var pais = await _paisService.GetByCodeAsync(code);
                if (pais != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(pais, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<Pais>(null, HttpStatusCode.NotFound, "No se encontro el pais");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<Pais>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
