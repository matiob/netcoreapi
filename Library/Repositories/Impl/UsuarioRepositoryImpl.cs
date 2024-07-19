using Library.Context;
using Library.Domains;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                var existe = _context.Usuarios.Any(u => u.Email == email);
                return existe;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
    }
}
