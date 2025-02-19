using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Interfaces
{
    public interface IProjectManagerService
    {
        Task<IEnumerable<ProjectManager>> GetAllAsync();
        Task<ProjectManager?> GetByIdAsync(int id);
        Task AddAsync(ProjectManager projectManager);
        Task UpdateAsync(ProjectManager projectManager);
        Task DeleteAsync(int id);
    }
}
