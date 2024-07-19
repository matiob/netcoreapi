using Library.Context;
using Library.Domains;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.Repositories.Impl
{
    public class LibroRepositoryImpl : ILibroRepository
    {
        private readonly LibraryContext _context;
        public LibroRepositoryImpl(LibraryContext context) 
        { 
            _context = context;
        }
        public async Task<List<Libro>> GetAllAsync()
        {
            try
            {
                return await _context.Libros
                    .Include(libro => libro.Autor) // join
                    .Include(libro => libro.Genero) // join
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Libro> GetByIdAsync(Guid id)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(libro => libro.Autor) // join
                    .Include(libro => libro.Genero) // join
                    .FirstOrDefaultAsync(libro => libro.Id == id);

                if (libro == null)
                {
                    throw new KeyNotFoundException("[R] Libro no encontrado");
                }
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Libro> GetByTitleAsync(string titulo)
        {
            try
            {
                var libro = await _context.Libros
                    .Include(libro => libro.Autor)
                    .Include(libro => libro.Genero)
                    .FirstOrDefaultAsync(libro => libro.Titulo == titulo);

                if (libro == null)
                {
                    throw new KeyNotFoundException("[R] Libro no encontrado");
                }
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Libro> AddAsync(Libro libro)
        {
            try
            {
                var entity = await _context.Libros.AddAsync(libro);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Libro> PutAsync(Libro libro)
        {
            try
            {
                // Bloque equivalente
                //_context.Entry(libro).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
                //return libro;

                var entity = _context.Libros.Update(libro);
                await _context.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
        public async Task<Libro> DeleteAsync(Libro libro)
        {
            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }

        public async Task<List<Libro>> GetBetweenDatesAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var libros = await _context.Libros
                    .Include(libro => libro.Autor)
                    .Include(libro => libro.Genero)
                    .Where(libro => libro.FechaPublicacion >= fechaInicio && libro.FechaPublicacion <= fechaFin)
                    .ToListAsync();

                return libros;
            }
            catch (Exception ex)
            {
                throw new Exception("[R] Error " + ex.Message);
            }
        }
    }
}
