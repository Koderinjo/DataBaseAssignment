using ProjectManagementConsole.Services;
using System;
using System.Threading.Tasks;

namespace ProjectManagementConsole
{
    public class MenuService
    {
        private readonly ProjectConsoleService _projectService;
        private readonly CustomerConsoleService _customerService;

        public MenuService(ProjectConsoleService projectService, CustomerConsoleService customerService)
        {
            _projectService = projectService;
            _customerService = customerService;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Project Management Console ===");
                Console.WriteLine("1) Manage Projects");
                Console.WriteLine("2) Manage Customers");
                Console.WriteLine("0) Exit");
                Console.Write("\nChoose an option: ");

                var choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        await ManageProjectsAsync();
                        break;
                    case "2":
                        await ManageCustomersAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ManageProjectsAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Projects ===");
                Console.WriteLine("1) List Projects");
                Console.WriteLine("2) Create Project");
                Console.WriteLine("3) Update Project");
                Console.WriteLine("4) Delete Project");
                Console.WriteLine("0) Back to Main Menu");
                Console.Write("\nChoose an option: ");

                var choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        await _projectService.ListProjectsAsync();
                        break;
                    case "2":
                        await _projectService.CreateProjectAsync();
                        break;
                    case "3":
                        await _projectService.UpdateProjectAsync();
                        break;
                    case "4":
                        await _projectService.DeleteProjectAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task ManageCustomersAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Manage Customers ===");
                Console.WriteLine("1) List Customers");
                Console.WriteLine("2) Create Customer");
                Console.WriteLine("3) Update Customer");
                Console.WriteLine("4) Delete Customer");
                Console.WriteLine("0) Back to Main Menu");
                Console.Write("\nChoose an option: ");

                var choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        await _customerService.ListCustomersAsync();
                        break;
                    case "2":
                        await _customerService.AddCustomerAsync();
                        break;
                    case "3":
                        await _customerService.UpdateCustomerAsync();
                        break;
                    case "4":
                        await _customerService.DeleteCustomerAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
