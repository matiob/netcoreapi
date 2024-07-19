using AutoMapper;
using Library.Domains;
using Library.DTOs.Request.Client;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Repositories;

namespace Library.Services.Impl
{
    public class GeneroServiceImpl : IGeneroService
    {
        private readonly IMapper _mapper;
        private readonly IGeneroRepository _generoRepository;

        public GeneroServiceImpl(IMapper mapper, IGeneroRepository generoRepository)
        {
            _mapper = mapper;
            _generoRepository = generoRepository;
        }
        public async Task<List<GeneroDTO>> GetAllAsync()
        {
            try
            {
                List<Genero> generos = await _generoRepository.GetAllAsync();
                List<GeneroDTO> result = _mapper.Map<List<GeneroDTO>>(generos);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<GeneroDTO> GetByIdAsync(int id)
        {
            try
            {
                var genero = await _generoRepository.GetByIdAsync(id);
                var response = _mapper.Map<GeneroDTO>(genero);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<GeneroDTO> GetByNameAsync(string name)
        {
            try
            {
                var genero = await _generoRepository.GetByNameAsync(name);
                var response = _mapper.Map<GeneroDTO>(genero);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<GeneroDTO> CreateAsync(CreateGeneroDTO dto)
        {
            try
            {
                var autor = _mapper.Map<Genero>(dto);
                var entity = await _generoRepository.AddAsync(autor);
                var response = _mapper.Map<GeneroDTO>(entity);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<GeneroDTO> UpdateAsync(UpdateGeneroDTO dto)
        {
            try
            {
                var dtoId = int.Parse(dto.Id);
                var genero = await _generoRepository.GetByIdAsync(dtoId);
                if (genero == null)
                {
                    throw new KeyNotFoundException("[S] Genero no encontrado");
                }

                _mapper.Map(dto, genero); // mapeo sin generación de instancia

                var generoEditado = await _generoRepository.PutAsync(genero);
                var response = _mapper.Map<GeneroDTO>(generoEditado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<GeneroDTO> DeleteAsync(int id)
        {
            try
            {
                var genero = await _generoRepository.GetByIdAsync(id);
                if (genero == null)
                {
                    throw new KeyNotFoundException("[S] Genero no encontrado");
                }
                var generoEliminado = await _generoRepository.DeleteAsync(genero);
                var response = _mapper.Map<GeneroDTO>(generoEliminado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
    }
}
