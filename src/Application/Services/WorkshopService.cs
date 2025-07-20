using Application.DTOs.Workshops;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "workshops";
        private const string ApiRelativeUrl = "places/workshops";

        public WorkshopService(IHttpClientService httpClientService, IMemoryCache cache)
        {
            _httpClientService = httpClientService;
            _cache = cache;
        }

        public async Task<List<WorkshopDto>> GetActiveWorkshopsAsync()
        {
            // Si ya está en caché, devolverlo directamente
            if (_cache.TryGetValue(CacheKey, out List<WorkshopDto> cachedWorkshops))
            {
                return cachedWorkshops;
            }

            // Si no está en caché, llamar a la API externa
            // Llama a la API y deserializa la respuesta a List<WorkshopDto>
            var response = await _httpClientService.GetAsync<List<WorkshopDto>>(ApiRelativeUrl);

            if (response is null)
                return new List<WorkshopDto>();

            // Guardar en caché por 30 minutos
            _cache.Set(CacheKey, response, TimeSpan.FromMinutes(30));

            return response;
        }


        public async Task<bool> ExistsAsync(int workshopId)
        {
            var workshops = await GetActiveWorkshopsAsync();

            // Si no hay talleres, simplemente retorna false en vez de lanzar excepción
            if (workshops == null || workshops.Count == 0)
            {
                return false;
            }

            var workshopExists = workshops.Any(w => w.Id == workshopId && w.Active);

            return workshopExists;
        }
    }

}