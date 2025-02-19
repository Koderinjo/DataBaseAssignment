using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Repositories
{
    public class ServiceRepository(AppDbContext context) : BaseRepository<Service>(context), IServiceRepository
    {
    }
}
