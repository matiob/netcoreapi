using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;

namespace Library.Services
{
    public interface ILibroService
    {
        public Task<List<LibroDTO>> GetAllAsync();
        public Task<List<LibroDTO>> GetBetweenDatesAsync(string start, string end);
        public Task<LibroDTO> GetByIdAsync(Guid id);
        public Task<LibroDTO> GetByTitleAsync(string title);
        public Task<LibroDTO> CreateAsync(CreateLibroDTO dto);
        public Task<LibroDTO> UpdateAsync(UpdateLibroDTO dto);
        public Task<LibroDTO> DeleteAsync(Guid id);
    }
}
