﻿using Application.Interfaces;
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
    }
}
