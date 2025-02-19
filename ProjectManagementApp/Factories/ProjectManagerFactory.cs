using ProjectManagementApp.Models;

namespace ProjectManagementApp.Factories
{
    public static class ProjectManagerFactory
    {
        public static ProjectManager Create(string name, string email)
        {
            return new ProjectManager { Name = name, Email = email };
        }
    }
}
