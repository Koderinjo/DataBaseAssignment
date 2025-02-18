using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProjectManagementApp.Models;
using ProjectManagementConsole.Helpers;

namespace ProjectManagementConsole.Services
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

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task CreateProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Create New Project ===\n");

            Console.Write("Enter project name: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Enter project number: ");
            var projectNumber = Console.ReadLine() ?? "";

            Console.Write("Enter project manager: ");
            var manager = Console.ReadLine() ?? "";

            Console.Write("Enter customer ID: ");
            int.TryParse(Console.ReadLine(), out int customerId);

            Console.Write("Enter project start date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Now;
            if (DateTime.TryParse(Console.ReadLine(), out var parsedStartDate))
                startDate = parsedStartDate;

            Console.Write("Enter project end date (yyyy-MM-dd, optional): ");
            DateTime? endDate = null;
            var endDateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(endDateInput) && DateTime.TryParse(endDateInput, out var parsedEndDate))
                endDate = parsedEndDate;

            Console.Write("Enter project status: ");
            var status = Console.ReadLine() ?? "Not Started";

            Console.Write("Enter total price: ");
            decimal totalPrice = 0;
            if (decimal.TryParse(Console.ReadLine(), out var parsedPrice))
                totalPrice = parsedPrice;

            var newProject = new Project
            {
                Name = name,
                ProjectNumber = projectNumber,
                StartDate = startDate,
                EndDate = endDate,
                Status = status,
                ProjectManager = manager,
                CustomerId = customerId,
                TotalPrice = totalPrice
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Project", newProject);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nProject created successfully!");
                }
                else
                {
                    Console.WriteLine($"\nFailed to create project. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError creating project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task UpdateProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Edit a Project ===");

            Console.Write("Enter the Project ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid ID. Press any key to return...");
                Console.ReadKey();
                return;
            }

            var existingProject = await _httpClient.GetFromJsonAsync<Project>($"api/Project/{projectId}");
            if (existingProject == null)
            {
                Console.WriteLine("Project not found. Press any key to return...");
                Console.ReadKey();
                return;
            }

            existingProject.Name = ConsoleHelper.GetUpdatedValue($"Current Name: {existingProject.Name}", existingProject.Name);
            existingProject.Status = ConsoleHelper.GetUpdatedValue($"Current Status: {existingProject.Status}", existingProject.Status);
            existingProject.ProjectManager = ConsoleHelper.GetUpdatedValue($"Current Project Manager: {existingProject.ProjectManager}", existingProject.ProjectManager);

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Project/{projectId}", existingProject);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("\nProject updated successfully!");
                }
                else
                {
                    Console.WriteLine($"\nFailed to update project.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        public async Task DeleteProjectAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Project ===\n");

            Console.Write("Enter the Project ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid ID. Press any key to return...");
                Console.ReadKey();
                return;
            }

            try
            {
                await _httpClient.DeleteAsync($"api/Project/{projectId}");
                Console.WriteLine("Project deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }
    }
}
