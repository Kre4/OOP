using Banks.UI;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var consoleInterface = new ConsoleInterface();
            consoleInterface.ShowMainMenu();
        }
    }
}
