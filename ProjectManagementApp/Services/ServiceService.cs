using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Services
{
    public class ServiceService(IServiceRepository repository) : IServiceService
    {
        private readonly IServiceRepository _repository = repository;

        public async Task<IEnumerable<Service>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Service?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);
        public async Task AddAsync(Service service) => await _repository.AddAsync(service);
        public async Task UpdateAsync(Service service) => await _repository.UpdateAsync(service);
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null) await _repository.DeleteAsync(entity);
        }
    }
}
