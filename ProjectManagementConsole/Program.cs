using System.Threading.Tasks;

namespace ProjectManagementConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var menuService = new MenuService();
            await menuService.RunAsync();
        }
    }
}
