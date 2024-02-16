using CRM.Models;

namespace API.Interfaces
{
    public interface IServiceRepository
    {
        Task<ICollection<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service?> GetByIdNoTrackingAsync(int id);
        Task<bool> CreateAsync(Service service);
        Task<bool> UpdateAsync(Service service);
        Task<bool> DeleteAsync(Service service);
        Task<bool> SaveAsync();
    }
}
