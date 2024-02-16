using CRM.Models;

namespace API.Interfaces
{
    public interface IBlogRepository
    {
        Task<ICollection<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<Blog?> GetByIdNoTrackingAsync(int id);
        Task<bool> CreateAsync(Blog blog);
        Task<bool> UpdateAsync(Blog blog);
        Task<bool> DeleteAsync(Blog blog);
        Task<bool> SaveAsync();
    }
}
