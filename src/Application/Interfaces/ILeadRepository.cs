using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILeadRepository
    {
        Task AddAsync(Lead lead);
        Task<List<Lead>> GetAllAsync();
    }
}
