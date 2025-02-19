using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class ProjectManagerService(IProjectManagerRepository repository) : IProjectManagerService
    {
        private readonly IProjectManagerRepository _repository = repository;

        public async Task<IEnumerable<ProjectManager>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<ProjectManager?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(ProjectManager projectManager) => await _repository.AddAsync(projectManager);
        public async Task UpdateAsync(ProjectManager projectManager) => await _repository.UpdateAsync(projectManager);
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) await _repository.DeleteAsync(entity);
        }
    }
}
