using Application.DTOs.Leads;

namespace Application.Interfaces
{
    public interface ILeadService
    {
        Task<List<LeadResponseDto>> GetAllAsync();
        Task<LeadResponseDto> AddAsync(LeadRequestDto request);
    }
}
