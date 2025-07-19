using Application.DTOs.Workshops;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "workshops";
        private const string ApiUrl = "https://dev.tecnomcrm.com/api/v1/places/workshops";

        public WorkshopService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;

            var authToken = Convert.ToBase64String(
                Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp")
            );

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authToken);
        }

        public async Task<List<WorkshopDto>> GetActiveWorkshopsAsync()
        {
            // Si ya está en caché, devolverlo directamente
            if (_cache.TryGetValue(CacheKey, out List<WorkshopDto> cachedWorkshops))
            {
                return cachedWorkshops;
            }

            // Si no está en caché, llamar a la API externa
            var response = await _httpClient.GetFromJsonAsync<List<WorkshopDto>>(ApiUrl);

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
