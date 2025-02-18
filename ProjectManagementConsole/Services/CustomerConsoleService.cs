using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Models;
using ProjectManagementConsole.Helpers;

namespace ProjectManagementConsole.Services
{
    public class CustomerConsoleService(ICustomerService customerService)
    {
        private readonly ICustomerService _customerService = customerService;

        public async Task ListCustomersAsync()
        {
            var customers = await _customerService.GetAllCustomersAsync();

            Console.Clear();
            Console.WriteLine("=== Customer List ===");
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Phone: {customer.Phone}");
            }
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task AddCustomerAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Add a New Customer ===");

            Console.Write("Enter Name: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine() ?? string.Empty;

            var newCustomer = new Customer
            {
                Name = name,
                Email = email,
                Phone = phone
            };

            try
            {
                await _customerService.AddCustomerAsync(newCustomer);
                Console.WriteLine("Customer added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task UpdateCustomerAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Edit a Customer ===");

            Console.Write("Enter the Customer ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid input. Press any key to return.");
                Console.ReadKey();
                return;
            }

            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            customer.Name = ConsoleHelper.GetUpdatedValue($"Current Name: {customer.Name}", customer.Name);
            customer.Email = ConsoleHelper.GetUpdatedValue($"Current Email: {customer.Email}", customer.Email);
            customer.Phone = ConsoleHelper.GetUpdatedValue($"Current Phone: {customer.Phone}", customer.Phone);

            try
            {
                await _customerService.UpdateCustomerAsync(customer);
                Console.WriteLine("Customer updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task DeleteCustomerAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Delete a Customer ===");

            Console.Write("Enter the Customer ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine("Invalid input. Press any key to return.");
                Console.ReadKey();
                return;
            }

            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                Console.WriteLine("Customer deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
