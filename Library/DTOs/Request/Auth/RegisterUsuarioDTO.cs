namespace Library.DTOs.Request.Auth
{
    public class RegisterUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
