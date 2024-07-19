
using Library.DTOs.Request.Auth;
using Library.DTOs.Response;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService) 
        { 
            _usuarioService = usuarioService;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> GetProfile()
        {
            var usuario = await _usuarioService.GetPerfilUsuarioAsync();
            if (usuario == null)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.NotFound, "Usuario no encontrado");
                return NotFound(response);
            }
            else
            {
                var response = ApiResponseWrapper.CreateResponse(usuario, HttpStatusCode.OK);
                return Ok(response);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<List<UsuarioDTO>>>> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                if (usuarios.Count > 0)
                {
                    var response = ApiResponseWrapper.CreateResponse(usuarios, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<List<UsuarioDTO>>(null, HttpStatusCode.NotFound, "No se encontraron usuarios");
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<List<UsuarioDTO>>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> GetById(string id)
        {
            try
            {
                var usuarioId = Guid.Parse(id);
                var usuario = await _usuarioService.GetByIdAsync(usuarioId);

                if (usuario != null)
                {
                    var response = ApiResponseWrapper.CreateResponse(usuario, HttpStatusCode.OK);
                    return Ok(response);
                }
                else
                {
                    var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.NotFound, "Usuario no encontrado");
                    return NotFound(response);
                }
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> Create([FromBody] RegisterUsuarioDTO usuarioReg)
        {
            try
            {
                var usuarioCreado = await _usuarioService.CreateAsync(usuarioReg);
                var response = ApiResponseWrapper.CreateResponse(usuarioCreado, HttpStatusCode.Created);
                return CreatedAtAction(nameof(GetById), new { id = usuarioCreado.Id }, response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "regular, Admin")]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> Update(string id, [FromBody] EditUsuarioDTO usuarioEdit)
        {
            var usuarioId = Guid.Parse(usuarioEdit.Id);
            var paramId = Guid.Parse(id);
            if (paramId != usuarioId)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }

            try
            {
                var usuarioEditado = await _usuarioService.UpdateAsync(usuarioEdit);
                var response = ApiResponseWrapper.CreateResponse(usuarioEditado);
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.NotFound, "Usuario no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> Delete(string id)
        {
            try
            {
                var usuarioId = Guid.Parse(id);
                var usuario = await _usuarioService.DeleteAsync(usuarioId);
                var response = ApiResponseWrapper.CreateResponse(usuario);
                return Ok(response);
            }
            catch (FormatException)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.BadRequest, "Formato de ID inválido");
                return BadRequest(response);
            }
            catch (KeyNotFoundException)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.NotFound, "Usuario no encontrado");
                return NotFound(response);
            }
            catch (Exception ex)
            {
                var response = ApiResponseWrapper.CreateResponse<UsuarioDTO>(null, HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, response);
            }
        }
    }
}
