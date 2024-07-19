using Library.DTOs.Request.Auth;
using Library.DTOs.Response;
using Library.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDTO loginDto)
        {
            var token = await _loginService.ValidateUserAsync(loginDto.UserName, loginDto.Password);
            if (token == null)
            {
                var resp = ApiResponseWrapper.CreateResponse<string>("", HttpStatusCode.Forbidden, "Usuario no autenticado");
                // return Forbid(); // no tiene sobre carga para enviar objeto
                return StatusCode((int)HttpStatusCode.Forbidden, resp);
            }
            var response = ApiResponseWrapper.CreateResponse(token);
            return response;
        }
    }
}
