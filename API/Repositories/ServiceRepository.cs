using API.Data;
using API.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ServiceRepository(ApplicationDbContext context) : IServiceRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ICollection<Service>> GetAllAsync()
        {
            return await _context.Services.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service?> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
