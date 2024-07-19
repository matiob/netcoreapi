using Library.Domains;
using System.Threading.Tasks;

namespace Library.Repositories
{
    public interface ILibroRepository
    {
        public Task<List<Libro>> GetAllAsync();
        public Task<List<Libro>> GetBetweenDatesAsync(DateTime fechaInicio, DateTime fechaFin);
        public Task<Libro> GetByIdAsync(Guid id);
        public Task<Libro> GetByTitleAsync(string title);
        public Task<Libro> AddAsync(Libro libro);
        public Task<Libro> PutAsync(Libro libro);
        public Task<Libro> DeleteAsync(Libro libro);
    }
}
