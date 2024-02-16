using API.Data;
using API.Interfaces;
using CRM.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BlogRepository(ApplicationDbContext context) : IBlogRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ICollection<Blog>> GetAllAsync()
        {
            return await _context.Blogs.OrderByDescending(b => b.CreatedDate).ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Blog?> GetByIdNoTrackingAsync(int id)
        {
            return await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<bool> CreateAsync(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
