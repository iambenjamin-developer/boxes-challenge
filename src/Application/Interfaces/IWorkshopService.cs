
using Application.DTOs.Workshops;

namespace Application.Interfaces
{
    public interface IWorkshopService
    {
        Task<List<WorkshopDto>> GetActiveWorkshopsAsync();
        Task<bool> ExistsAsync(int workshopId, CancellationToken cancellationToken = default);
    }
}
