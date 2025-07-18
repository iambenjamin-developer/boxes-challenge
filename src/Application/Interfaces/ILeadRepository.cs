using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILeadRepository
    {
        Task<List<Lead>> GetAllAsync();
        Task<Lead> AddAsync(Lead entity);
    }
}
