using System;
using System.Diagnostics.CodeAnalysis;

namespace MyobPayslipCalculator.Services
{
    [ExcludeFromCodeCoverage]
    public static class LogService 
    {
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void Success(string message)
        {            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
