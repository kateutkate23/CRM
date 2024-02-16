using API.Data;
using API.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ICollection<Project>> GetAllAsync()
        {
            return await _context.Projects.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Project project)
        {
            _context.Projects.Remove(project);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
