using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;

namespace Library.Services
{
    public interface IGeneroService
    {
        public Task<List<GeneroDTO>> GetAllAsync();
        public Task<GeneroDTO> GetByIdAsync(int id);
        public Task<GeneroDTO> GetByNameAsync(string name);
        public Task<GeneroDTO> CreateAsync(CreateGeneroDTO dto);
        public Task<GeneroDTO> UpdateAsync(UpdateGeneroDTO dto);
        public Task<GeneroDTO> DeleteAsync(int id);
    }
}
