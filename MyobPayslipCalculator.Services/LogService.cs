using System;
using System.Diagnostics.CodeAnalysis;

namespace MyobPayslipCalculator.Services
{
    [ExcludeFromCodeCoverage]
    public static class LogService 
    {

        public static void Welcome(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();            
        }

        public static void Success(string message)
        {            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();            
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();            
        }
    }
}
