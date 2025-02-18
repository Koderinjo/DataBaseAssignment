namespace ProjectManagementConsole.Helpers
{
    public static class ConsoleHelper
    {
        public static string GetUpdatedValue(string prompt, string currentValue)
        {
            Console.WriteLine(prompt);
            Console.Write("New value (press Enter to keep current): ");
            string newValue = Console.ReadLine()?.Trim() ?? string.Empty;
            return string.IsNullOrWhiteSpace(newValue) ? currentValue : newValue;
        }
    }
}
