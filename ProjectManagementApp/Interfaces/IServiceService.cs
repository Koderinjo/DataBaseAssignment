using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
