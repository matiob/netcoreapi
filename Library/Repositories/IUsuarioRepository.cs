using Library.Domains;

namespace Library.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsuarioByNameAndPasswordAsync(string nombre, string password);
        Task<Usuario> GetUsuarioByIdAsync(Guid id);

        Task<bool> GetUsuarioByEmailAsync(string email);

        Task<Usuario> AddAsync(Usuario usuario);

        Task<Usuario> PutAsync(Usuario usuario);

        Task<Usuario> DeleteAsync(Usuario usuario);
    }
}
