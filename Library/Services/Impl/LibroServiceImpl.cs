using AutoMapper;
using Library.Domains;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Repositories;

namespace Library.Services.Impl
{
    public class LibroServiceImpl : ILibroService
    {
        private readonly IMapper _mapper;
        private readonly ILibroRepository _libroRepository;
        private readonly IAutorService _autorService;
        private readonly IGeneroService _generoService;

        public LibroServiceImpl(IMapper mapper, ILibroRepository libroRepository, IAutorService autorService, IGeneroService generoService) 
        {
            _mapper = mapper;
            _libroRepository = libroRepository;
            _autorService = autorService;
            _generoService = generoService;
        }
        public async Task<List<LibroDTO>> GetAllAsync()
        {
            try
            {
                List<Libro> libros = await _libroRepository.GetAllAsync();
                List<LibroDTO> result = _mapper.Map<List<LibroDTO>>(libros);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<LibroDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var libro = await _libroRepository.GetByIdAsync(id);
                var response = _mapper.Map<LibroDTO>(libro);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<LibroDTO> GetByTitleAsync(string title)
        {
            try
            {
                var libro = await _libroRepository.GetByTitleAsync(title);
                var response = _mapper.Map<LibroDTO>(libro);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<LibroDTO> CreateAsync(CreateLibroDTO dto)
        {
            try
            {
                var libro = _mapper.Map<Libro>(dto);
                libro = await SetAutor(libro);
                libro = await SetGenero(libro);
                var entity = await _libroRepository.AddAsync(libro);
                var response = _mapper.Map<LibroDTO>(entity);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<LibroDTO> UpdateAsync(UpdateLibroDTO dto)
        {
            try
            {
                var dtoId = Guid.Parse(dto.Id);
                var libro = await _libroRepository.GetByIdAsync(dtoId);
                if (libro == null)
                {
                    throw new KeyNotFoundException("[S] Libro no encontrado");
                }

                _mapper.Map(dto, libro); // mapeo sin generación de instancia

                var libroEditado = await _libroRepository.PutAsync(libro);
                var response = _mapper.Map<LibroDTO>(libroEditado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<LibroDTO> DeleteAsync(Guid id)
        {
            try
            {
                var libro = await _libroRepository.GetByIdAsync(id);
                if (libro == null)
                {
                    throw new KeyNotFoundException("[S] Libro no encontrado");
                }
                var libroEliminado = await _libroRepository.DeleteAsync(libro);
                var response = _mapper.Map<LibroDTO>(libroEliminado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<List<LibroDTO>> GetBetweenDatesAsync(string fechaInicio, string fechaFin)
        {
            try
            {
                var start = DateTime.Parse(fechaInicio);
                var end = DateTime.Parse(fechaFin);

                var libros = await _libroRepository.GetBetweenDatesAsync(start, end);
                var librosDto = _mapper.Map<List<LibroDTO>>(libros);
                return librosDto;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        private async Task<Libro> SetAutor(Libro libro)
        {
            var autor = await _autorService.GetAutorByIdAsync(libro.AutorId);
            if (autor == null)
            {
                throw new KeyNotFoundException("[S] Autor no encontrado");
            }
            libro.Autor = autor;
            return libro;
        }
        private async Task<Libro> SetGenero(Libro libro)
        {
            var genero = await _generoService.GetByIdAsync(libro.GeneroId);
            if (genero == null)
            {
                throw new KeyNotFoundException("[S] Genero no encontrado");
            }
            _mapper.Map(libro.Genero, genero); // mapeo sin instancia
            return libro;
        }
    }
}
