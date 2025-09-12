using System.Runtime.CompilerServices;
using Unimake;

namespace RuntimeHelperConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"IsInteractive: {RuntimeHelper.IsInteractive}");
            Console.WriteLine($"IsDotNetHost : {RuntimeHelper.IsDotNetHost}");
            Console.WriteLine($"HasConsole   : {RuntimeHelper.HasConsole}");
            Console.ReadKey();
            Environment.Exit(0);    
        }
    }
}
