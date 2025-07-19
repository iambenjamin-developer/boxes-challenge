using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly AppDbContext _context;

        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Lead>> GetAllAsync()
        {
            return await _context.Leads.ToListAsync();
        }


        public async Task<Lead> AddAsync(Lead entity)
        {
            await _context.Leads.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Lead> GetByIdAsync(long id)
        {
            return await _context.Leads
                .Include(l => l.Contact)
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

    }
}
