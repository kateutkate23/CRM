using API.Data;
using API.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ApplicationRepository(ApplicationDbContext context) : IApplicationRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ICollection<Application>> GetAllAsync()
        {
            return await _context.Applications.OrderBy(a => a.CreatedDate).ToListAsync();
        }

        public async Task<ICollection<Application>> GetBetweenDatesAsync(DateTime begin, DateTime end)
        {
            end = end.AddDays(1);

            return await _context.Applications.Where(a => a.CreatedDate >= begin && a.CreatedDate < end).ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            return await _context.Applications.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Application?> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Applications.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> CreateAsync(Application application)
        {
            await _context.Applications.AddAsync(application);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Application application)
        {
            _context.Applications.Update(application);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Application application)
        {
            _context.Applications.Remove(application);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
