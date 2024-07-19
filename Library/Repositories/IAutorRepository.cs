using Library.Domains;

namespace Library.Repositories
{
    public interface IAutorRepository
    {
        public Task<List<Autor>> GetAllAsync();
        public Task<Autor> GetByIdAsync(Guid id);
        public Task<Autor> GetByNameAsync(string name);
        public Task<Autor> AddAsync(Autor autor);
        public Task<Autor> PutAsync(Autor autor);
        public Task<Autor> DeleteAsync(Autor autor);
    }
}
