using Library.Domains;
using Library.DTOs.Request.Auth;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;

namespace Library.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync();

        Task<Usuario> GetByIdAsync(Guid id);
        Task<UsuarioDTO> GetUsuarioByNameAndPasswordAsync(string name, string password);

        Task<UsuarioDTO> GetPerfilUsuarioAsync();

        Task<UsuarioDTO> CreateAsync(RegisterUsuarioDTO dto);
        Task<UsuarioDTO> UpdateAsync(EditUsuarioDTO dto);
        Task<UsuarioDTO> DeleteAsync(Guid id);
    }
}
