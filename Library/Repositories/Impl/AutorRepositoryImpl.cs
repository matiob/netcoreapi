using Library.Context;
using Library.Domains;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Impl
{
    public class AutorRepositoryImpl : IAutorRepository
    {
        private readonly LibraryContext _context;
        public AutorRepositoryImpl(LibraryContext context)
        {
            _context = context;
        }
        public async Task<List<Autor>> GetAllAsync()
        {
            try
            {
                return await _context.Autores
                    //.Include(autor => autor.Pais) // join
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Autor> GetByIdAsync(Guid id)
        {
            try
            {
                var autor = await _context.Autores
                    //.Include(autor => autor.Pais)
                    .FirstOrDefaultAsync(autor => autor.Id == id);

                if (autor == null)
                {
                    throw new KeyNotFoundException("[R] Autor no encontrado");
                }
                return autor;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Autor> GetByNameAsync(string nombre)
        {
            try
            {
                var autor = await _context.Autores
                    //.Include(autor => autor.Pais)
                    .FirstOrDefaultAsync(autor => autor.Nombre == nombre);

                if (autor == null)
                {
                    throw new KeyNotFoundException("[R] Autor no encontrado");
                }
                return autor;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Autor> AddAsync(Autor autor)
        {
            try
            {
                var entity = await _context.Autores.AddAsync(autor);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Autor> PutAsync(Autor autor)
        {
            try
            {
                var entity = _context.Autores.Update(autor);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Autor> DeleteAsync(Autor autor)
        {
            try
            {
                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
                return autor;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
    }
}
