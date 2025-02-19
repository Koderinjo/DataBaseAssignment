using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class ProjectService(IProjectRepository _projectRepository, AppDbContext _context) : IProjectService
    {
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _projectRepository.GetProjectByIdAsync(id);
        }

        public async Task AddProjectAsync(Project project)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _projectRepository.AddProjectAsync(project);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateProjectAsync(Project project)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _projectRepository.UpdateProjectAsync(project);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteProjectAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _projectRepository.DeleteProjectAsync(id);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
