using ProjectManagementApp.Models;

namespace ProjectManagementApp.Factories
{
    public static class ServiceFactory
    {
        public static Service Create(string name, decimal price)
        {
            return new Service { Name = name, Price = price };
        }
    }
}
