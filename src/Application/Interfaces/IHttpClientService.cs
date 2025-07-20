
namespace Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<T?> GetAsync<T>(string url, CancellationToken cancellationToken = default);
    }
}
