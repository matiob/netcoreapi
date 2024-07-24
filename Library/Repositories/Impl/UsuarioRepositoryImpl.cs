using Library.Context;
using Library.Domains;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Impl
{
    public class UsuarioRepositoryImpl : IUsuarioRepository
    {
        private readonly LibraryContext _context;

        public UsuarioRepositoryImpl(LibraryContext libraryContext) 
        {
            _context = libraryContext;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(Guid id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNameAndPasswordAsync(string nombre, string password)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Nombre == nombre && usuario.Password == password);
                if (usuario == null)
                {
                    throw new KeyNotFoundException("Usuario no encontrado");
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            try
            {
                var entity = await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<Usuario> PutAsync(Usuario usuario)
        {
            try
            {
                var entity = _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<Usuario> DeleteAsync(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<bool> GetUsuarioByEmailAsync(string email)
        {
            try
            {
                var existe = await _context.Usuarios.AnyAsync(u => u.Email.Equals(email));
                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<List<Usuario>> GetUsuariosSinRolConEmail(string rol)
        {
            // Construcción de una consulta LINQ: es una IQueryable<int>
            // todavía no se ejecuta en la DB, es una representación de la consulta
            var usuariosIds = _context.Usuarios
                .Where(x => x.Rol == rol).Select(x => x.Id); 

            var usuariosSinRolAndEmail = await _context.Usuarios
                .Where(x => !usuariosIds.Contains(x.Id) && x.Email != null)
                .ToListAsync();

            // En un paso
            //var albanilesNotInObra2 = await _context.Albaniles
            //    .Where(a => a.Activo && !_context.AlbanilesXObras.Any(ao => ao.IdObra == obraId && ao.IdAlbanil == a.Id))
            //    .ToListAsync();

            return usuariosSinRolAndEmail;

        }
    }
}
