using ProjectManagementConsole.Services;
using System;
using System.Threading.Tasks;

namespace ProjectManagementConsole
{
   
    public class MenuService
    {
        private readonly ProjectConsoleService _projectService;

        public MenuService()
        {
            _projectService = new ProjectConsoleService();
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Project Management Console ===");
                Console.WriteLine("1) List Projects");
                Console.WriteLine("2) Create Project");
                Console.WriteLine("3) Update Project");
                Console.WriteLine("4) Delete Project (optional)");
                Console.WriteLine("0) Exit");
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
    }
}
