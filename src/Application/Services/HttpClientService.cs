using Application.Interfaces;
using Polly.CircuitBreaker;
using Polly.Timeout;
using System.Text.Json;

namespace Application.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService(HttpClient client)
        {
            _client = client;
        }


        public async Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _client.GetAsync(url, cancellationToken);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<T>(jsonString, options);
                return result;
            }
            catch (TimeoutRejectedException)
            {
                string msg = "504 ⏳ Tiempo agotado para consultar la API (mayor a 10 segundos)";
                throw;
            }
            catch (BrokenCircuitException)
            {
                string msg = "503 ⛔ Circuito abierto - la API no está disponible.";
                throw;
            }
            catch (Exception ex)
            {
                string msg = "500 💥 Error inesperado";
                throw;
            }
        }
    }
}
