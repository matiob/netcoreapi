using AutoMapper;
using Library.Domains;
using Library.DTOs.Exceptions;
using Library.DTOs.Request.Client;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;
using Library.DTOs.Response;
using Library.Repositories;

namespace Library.Services.Impl
{
    public class AutorServiceImpl : IAutorService
    {
        private readonly IMapper _mapper;
        private readonly IAutorRepository _autorRepository;
        private readonly IPaisRepository _paisRepository;

        public AutorServiceImpl(IMapper mapper, IAutorRepository autorRepository, IPaisRepository paisRepository)
        {
            _mapper = mapper;
            _autorRepository = autorRepository;
            _paisRepository = paisRepository;
        }
        public async Task<List<AutorDTO>> GetAllAsync()
        {
            try
            {
                List<Autor> autores = await _autorRepository.GetAllAsync();
                autores = await CargarPais(autores);
                List<AutorDTO> result = _mapper.Map<List<AutorDTO>>(autores);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<AutorDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var autor = await _autorRepository.GetByIdAsync(id);
                var autorList = new List<Autor> { autor };
                autorList = await CargarPais(autorList);
                autor = autorList.FirstOrDefault();
                var response = _mapper.Map<AutorDTO>(autor);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<Autor> GetAutorByIdAsync(Guid id)
        {
            try
            {
                var autor = await _autorRepository.GetByIdAsync(id);
                var autorList = new List<Autor> { autor };
                autorList = await CargarPais(autorList);
                autor = autorList.FirstOrDefault();
                return autor;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<AutorDTO> GetByNameAsync(string name)
        {
            try
            {
                var autor = await _autorRepository.GetByNameAsync(name);
                var autorList = new List<Autor> { autor };
                autorList = await CargarPais(autorList);
                autor = autorList.FirstOrDefault();
                var response = _mapper.Map<AutorDTO>(autor);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<AutorDTO> CreateAsync(CreateAutorDTO dto)
        {
            try
            {
                var autorExistente = GetByNameAsync(dto.Nombre);
                if (autorExistente != null)
                {
                    //throw new InvalidOperationException($"Ya existe un autor con el nombre '{dto.Nombre}'.");
                    throw new AutorExistenteException(dto.Nombre);
                }
                var autor = _mapper.Map<Autor>(dto);
                if (autor.PaisId != null)
                {
                    var pais = _paisRepository.GetByCodeAsync(autor.PaisId);
                    if (pais == null)
                    {
                        throw new KeyNotFoundException("[S] Pais no encontrado");
                    }
                }
                var entity = await _autorRepository.AddAsync(autor);
                var response = _mapper.Map<AutorDTO>(entity);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<AutorDTO> UpdateAsync(UpdateAutorDTO dto)
        {
            try
            {
                var dtoId = Guid.Parse(dto.Id);
                var autor = await _autorRepository.GetByIdAsync(dtoId);
                if (autor == null)
                {
                    throw new KeyNotFoundException("[S] Autor no encontrado");
                }
                if (autor.PaisId != null)
                {
                    var pais = _paisRepository.GetByCodeAsync(autor.PaisId);
                    if (pais == null)
                    {
                        throw new KeyNotFoundException("[S] Pais no encontrado");
                    }
                }

                _mapper.Map(dto, autor); // mapeo sin generación de instancia

                var autorEditado = await _autorRepository.PutAsync(autor);
                var response = _mapper.Map<AutorDTO>(autorEditado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
        public async Task<AutorDTO> DeleteAsync(Guid id)
        {
            try
            {
                var autor = await _autorRepository.GetByIdAsync(id);
                if (autor == null)
                {
                    throw new KeyNotFoundException("[S] Libro no encontrado");
                }
                var autorEliminado = await _autorRepository.DeleteAsync(autor);
                var response = _mapper.Map<AutorDTO>(autorEliminado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        private async Task<List<Autor>> CargarPais(List<Autor> autores)
        {
            List<PaisDTO> paises = await _paisRepository.GetAllAsync();

            // Asigna el país correspondiente a cada autor
            foreach (var autor in autores)
            {
                var pais = paises.FirstOrDefault(p => p.ccn3 == autor.PaisId);

                if (pais != null)
                {
                    autor.Pais = _mapper.Map<Pais>(pais);
                }
            }

            return autores;
        }

    }
}
