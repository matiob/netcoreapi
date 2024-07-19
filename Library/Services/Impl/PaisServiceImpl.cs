using AutoMapper;
using Library.Domains;
using Library.DTOs.Request.Client;
using Library.DTOs.Response;
using Library.Repositories;

namespace Library.Services.Impl
{
    public class PaisServiceImpl : IPaisService
    {
        private readonly IPaisRepository _paisRepository;
        private readonly IMapper _mapper;

        public PaisServiceImpl(IPaisRepository paisRepository, IMapper mapper) 
        {
            _paisRepository = paisRepository;
            _mapper = mapper;
        }
        public async Task<List<Pais>> GetAllAsync()
        {
            try
            {
                List<PaisDTO> paises = await _paisRepository.GetAllAsync();
                List<Pais> result = _mapper.Map<List<Pais>>(paises);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<Pais> GetByCodeAsync(string code)
        {
            try
            {
                PaisDTO pais = await _paisRepository.GetByCodeAsync(code);
                Pais result = _mapper.Map<Pais>(pais);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }
    }
}
