using Library.Domains;
using Library.DTOs.Request.Client;
using System.Diagnostics.Metrics;

namespace Library.Repositories.Impl
{
    public class PaisRepositoryImpl : IPaisRepository
    {
        private readonly HttpClient _httpClient;

        public PaisRepositoryImpl(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CountryApi");
        }

        public async Task<List<PaisDTO>> GetAllAsync()
        {
            try
            {
                var paises = await _httpClient.GetFromJsonAsync<List<PaisDTO>>("all");

                //MAPPER CASERO
                //var list = new List<Country>();
                //foreach (var item in response)
                //{
                //    list.Add(new Country
                //    {
                //        Id = item.ccn3,
                //        Name = item.Name.official
                //    });
                //}

                //MAPPER
                //var paises = response.Select(x => new Pais
                //    {
                //        Id = int.Parse(x.ccn3),
                //        Nombre = x.Name.official
                //    }).ToList();

                if (paises == null)
                {
                    throw new KeyNotFoundException("[C] Paises no encontrados");
                }

                return paises;
            }
            catch (Exception ex)
            {
                return new List<PaisDTO>();
            }
        }

        public async Task<PaisDTO> GetByCodeAsync(string code)
        {
            try
            {
                var paises = await _httpClient.GetFromJsonAsync<List<PaisDTO>>($"alpha/{code}");

                if (paises != null && paises.Any())
                {
                    var pais = paises.First();
                    return pais;
                }
                else
                {
                    throw new KeyNotFoundException("[C] Pais no encontrado");
                }

            }
            catch (Exception ex)
            {
                return new PaisDTO();
            }
        }

        public Task<PaisDTO> UpdateAsync(Pais pais)
        {
            throw new NotImplementedException();
        }
        public Task<PaisDTO> CreateAsync(Pais pais)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
