
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Services.Auth
{
    public class LoginServiceImpl : ILoginService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly JwtHelper _jwtHelper;

        public LoginServiceImpl(IUsuarioService usuarioService, JwtHelper jwtHelper) 
        {
            _usuarioService = usuarioService;
            _jwtHelper = jwtHelper;
        }
        public async Task<string> ValidateUserAsync(string username, string password)
        {
            var usuario = await _usuarioService.GetUsuarioByNameAndPasswordAsync(username, password);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            } else
            {
                var id = Guid.Parse(usuario.Id);
                var token = _jwtHelper.GenerateJwtToken(usuario.Nombre, id, usuario.Rol);
                return token;
            }

           

        }
    }
}
