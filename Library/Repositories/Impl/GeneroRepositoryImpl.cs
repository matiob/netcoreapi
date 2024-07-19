using Library.Context;
using Library.Domains;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories.Impl
{
    public class GeneroRepositoryImpl : IGeneroRepository
    {
        private readonly LibraryContext _context;
        public GeneroRepositoryImpl(LibraryContext context)
        {
            _context = context;
        }
        public async Task<List<Genero>> GetAllAsync()
        {
            try
            {
                return await _context.Generos
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Genero> GetByIdAsync(int id)
        {
            try
            {
                var genero = await _context.Generos
                    .FirstOrDefaultAsync(genero => genero.Id == id);

                if (genero == null)
                {
                    throw new KeyNotFoundException("[R] Genero no encontrado");
                }
                return genero;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Genero> GetByNameAsync(string nombre)
        {
            try
            {
                var genero = await _context.Generos.FirstOrDefaultAsync(genero => genero.Nombre == nombre);

                if (genero == null)
                {
                    throw new KeyNotFoundException("[R] Autor no encontrado");
                }
                return genero;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Genero> AddAsync(Genero genero)
        {
            try
            {
                var entity = await _context.Generos.AddAsync(genero);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Genero> PutAsync(Genero genero)
        {
            try
            {
                var entity = _context.Generos.Update(genero);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Genero> DeleteAsync(Genero genero)
        {
            try
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
                return genero;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
    }
}
