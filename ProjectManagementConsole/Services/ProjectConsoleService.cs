using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProjectManagementApp.Models;

namespace ProjectManagementConsole
{
    public class ProjectConsoleService
    {
        private readonly HttpClient _httpClient;

        public ProjectConsoleService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5025")
            };
        }

        public async Task ListProjectsAsync()
        {
            Console.Clear();
            Console.WriteLine("=== List of Projects ===\n");

            try
            {
                var projects = await _httpClient.GetFromJsonAsync<List<Project>>("api/Project");

                if (projects == null || projects.Count == 0)
                {
                    Console.WriteLine("No projects found.");
                }
                else
                {
                    foreach (var p in projects)
                    {
                        Console.WriteLine(
                            $"ID: {p.Id}, " +
                            $"Name: {p.Name}, " +
                            $"Number: {p.ProjectNumber}, " +
                            $"Manager: {p.ProjectManager}, " +
                            $"Status: {p.Status}, " +
                            $"CustomerID: {p.CustomerId}, " +
                            $"Price: {p.TotalPrice}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        public async Task CreateProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Create New Project ===\n");

            try
            {
                Console.Write("Enter project name: ");
                var name = Console.ReadLine() ?? "";

                Console.Write("Enter project number: ");
                var projectNumber = Console.ReadLine() ?? "";

                Console.Write("Enter project manager: ");
                var manager = Console.ReadLine() ?? "";

                Console.Write("Enter customer ID (default=1 if unsure): ");
                var custIdInput = Console.ReadLine();
                int customerId = 1;
                if (int.TryParse(custIdInput, out int tempId))
                    customerId = tempId;

                var newProject = new Project
                {
                    Name = name,
                    ProjectNumber = projectNumber,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    Status = "Not Started",
                    ProjectManager = manager,
                    CustomerId = customerId,
                    TotalPrice = 1000.0M
                };

                var response = await _httpClient.PostAsJsonAsync("api/Project", newProject);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Project created successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to create project. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        public async Task UpdateProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Detailed View: Edit a Project ===\n");

            Console.Write("Enter the Project ID to edit: ");
            var input = Console.ReadLine() ?? "";
            if (!int.TryParse(input, out int projectId))
            {
                Console.WriteLine("Invalid ID. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                var existingProject = await _httpClient.GetFromJsonAsync<Project>($"api/Project/{projectId}");
                if (existingProject == null)
                {
                    Console.WriteLine("Project not found. Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Current Name: {existingProject.Name}");
                Console.Write("New name (press Enter to keep current): ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    existingProject.Name = newName;

                Console.WriteLine($"Current Start Date: {existingProject.StartDate}");
                Console.Write("New Start Date (press Enter to keep current, format yyyy-MM-dd): ");
                var newStart = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStart) && DateTime.TryParse(newStart, out var parsedStart))
                    existingProject.StartDate = parsedStart;

                Console.WriteLine($"Current End Date: {existingProject.EndDate}");
                Console.Write("New End Date (press Enter to keep current, format yyyy-MM-dd): ");
                var newEnd = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEnd) && DateTime.TryParse(newEnd, out var parsedEnd))
                    existingProject.EndDate = parsedEnd;

                Console.WriteLine($"Current Status: {existingProject.Status}");
                Console.Write("New status (press Enter to keep current): ");
                var newStatus = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newStatus))
                    existingProject.Status = newStatus;

                Console.WriteLine($"Current Project Manager: {existingProject.ProjectManager}");
                Console.Write("New project manager (press Enter to keep current): ");
                var newManager = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newManager))
                    existingProject.ProjectManager = newManager;

                Console.WriteLine($"Current CustomerId: {existingProject.CustomerId}");
                Console.Write("New CustomerId (press Enter to keep current): ");
                var newCustId = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCustId) && int.TryParse(newCustId, out var parsedCustId))
                    existingProject.CustomerId = parsedCustId;

                Console.WriteLine($"Current TotalPrice: {existingProject.TotalPrice}");
                Console.Write("New TotalPrice (press Enter to keep current): ");
                var newPrice = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPrice) && decimal.TryParse(newPrice, out var parsedPrice))
                    existingProject.TotalPrice = parsedPrice;

                Console.WriteLine("\nPress 'S' to Save changes, or 'C' to Cancel.");
                var choice = Console.ReadKey().KeyChar;
                if (char.ToUpper(choice) != 'S')
                {
                    Console.WriteLine("\nEdit canceled. Press any key to return...");
                    Console.ReadKey();
                    return;
                }

                var response = await _httpClient.PutAsJsonAsync($"api/Project/{projectId}", existingProject);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nProject updated successfully!");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"\nFailed to update project. {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        public async Task DeleteProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Project ===\n");

            Console.Write("Enter the Project ID to delete: ");
            var input = Console.ReadLine() ?? "";

            if (!int.TryParse(input, out int projectId))
            {
                Console.WriteLine("Invalid ID. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                var response = await _httpClient.DeleteAsync($"api/Project/{projectId}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Project with ID {projectId} deleted successfully!");
                }
                else
                {
                    Console.WriteLine($"Failed to delete project. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
