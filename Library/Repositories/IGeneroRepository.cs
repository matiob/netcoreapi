using Library.Domains;

namespace Library.Repositories
{
    public interface IGeneroRepository
    {
        public Task<List<Genero>> GetAllAsync();
        public Task<Genero> GetByIdAsync(int id);
        public Task<Genero> GetByNameAsync(string name);
        public Task<Genero> AddAsync(Genero autor);
        public Task<Genero> PutAsync(Genero autor);
        public Task<Genero> DeleteAsync(Genero autor);
    }
}
