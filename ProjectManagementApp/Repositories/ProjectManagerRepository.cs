using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Repositories
{
    public class ProjectManagerRepository(AppDbContext context) : BaseRepository<ProjectManager>(context), IProjectManagerRepository
    {
    }
}
