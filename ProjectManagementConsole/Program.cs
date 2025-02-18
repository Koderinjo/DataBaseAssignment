using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementApp.Data;
using ProjectManagementApp.Interfaces;
using ProjectManagementApp.Repositories;
using ProjectManagementApp.Services;
using ProjectManagementConsole;
using ProjectManagementConsole.Services;

class Program
{
    static async Task Main(string[] args)
    {
        // Load configuration (appsettings.json)
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Ensures correct directory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Setup DI container
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ProjectConsoleService>();
        services.AddScoped<CustomerConsoleService>();
        services.AddScoped<MenuService>();

        var serviceProvider = services.BuildServiceProvider();

        // Run the menu
        var menuService = serviceProvider.GetRequiredService<MenuService>();
        await menuService.RunAsync();
    }
}
