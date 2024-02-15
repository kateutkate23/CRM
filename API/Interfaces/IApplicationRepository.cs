using CRM.Models;

namespace API.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ICollection<Application>> GetAllAsync();
        Task<ICollection<Application>> GetBetweenDatesAsync(DateTime begin, DateTime end);
        Task<Application?> GetByIdAsync(int id);
        Task<Application?> GetByIdNoTrackingAsync(int id);
        Task<bool> CreateAsync(Application application);
        Task<bool> UpdateAsync(Application application);
        Task<bool> DeleteAsync(Application application);
        Task<bool> SaveAsync();
    }
}
