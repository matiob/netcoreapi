using Library.Domains;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;

namespace Library.Services
{
    public interface IAutorService
    {
        public Task<List<AutorDTO>> GetAllAsync();
        public Task<AutorDTO> GetByIdAsync(Guid id);
        public Task<Autor> GetAutorByIdAsync(Guid id);
        public Task<AutorDTO> GetByNameAsync(string name);
        public Task<AutorDTO> CreateAsync(CreateAutorDTO dto);
        public Task<AutorDTO> UpdateAsync(UpdateAutorDTO dto);
        public Task<AutorDTO> DeleteAsync(Guid id);
    }
}
