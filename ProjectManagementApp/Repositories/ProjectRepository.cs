using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class ProjectRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository
    {
        public async Task<IEnumerable<Project>> GetAllProjectsAsync() => await GetAllAsync();
        public async Task<Project?> GetProjectByIdAsync(int id) => await GetByIdAsync(id);
        public async Task AddProjectAsync(Project project) => await AddAsync(project);
        public async Task UpdateProjectAsync(Project project) => await UpdateAsync(project);
        public async Task DeleteProjectAsync(int id)
        {
            var project = await GetByIdAsync(id);
            if (project != null) await DeleteAsync(project);
        }
    }
}
