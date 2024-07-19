using Library.Domains;
using Library.DTOs.Request.Client;
using System.Diagnostics.Metrics;

namespace Library.Repositories
{
    public interface IPaisRepository
    {
        Task<List<PaisDTO>> GetAllAsync();
        Task<PaisDTO> GetByCodeAsync(string code);
        Task<PaisDTO> CreateAsync(Pais pais);
        Task<PaisDTO> UpdateAsync(Pais pais);
        Task<bool> DeleteAsync(Guid id);
    }
}
