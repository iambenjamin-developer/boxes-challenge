using Application.DTOs.Workshops;
using Application.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly HttpClient _httpClient;

        public WorkshopService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            var authToken = Convert.ToBase64String(
                Encoding.ASCII.GetBytes("max@tecnom.com.ar:b0x3sApp")
            );
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authToken);
        }

        public async Task<List<WorkshopDto>> GetActiveWorkshopsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<WorkshopDto>>(
                "https://dev.tecnomcrm.com/api/v1/places/workshops"
            );
            //return response.Select(x => x.Id).ToList();

            return response;
        }
    }

}
