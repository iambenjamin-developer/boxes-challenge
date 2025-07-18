
using Application.DTOs.Workshops;

namespace Application.Interfaces
{
    public interface IWorkshopService
    {
        Task<List<WorkshopDto>> GetActiveWorkshopsAsync();
    }
}
