using Library.Domains;

namespace Library.Services
{
    public interface IPaisService
    {
        Task<List<Pais>> GetAllAsync();
        Task<Pais> GetByCodeAsync(string code);
    }
}
