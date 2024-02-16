using CRM.Models;

namespace API.Interfaces
{
    public interface IProjectRepository
    {
        Task<ICollection<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project?> GetByIdNoTrackingAsync(int id);
        Task<bool> CreateAsync(Project project);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Project project);
        Task<bool> SaveAsync();
    }
}
