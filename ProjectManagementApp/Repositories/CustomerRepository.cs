using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Repositories
{
    public class CustomerRepository(AppDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() => await GetAllAsync();
        public async Task<Customer?> GetCustomerByIdAsync(int id) => await GetByIdAsync(id);
        public async Task AddCustomerAsync(Customer customer) => await AddAsync(customer);
        public async Task UpdateCustomerAsync(Customer customer) => await UpdateAsync(customer);
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer != null) await DeleteAsync(customer);
        }
    }
}
