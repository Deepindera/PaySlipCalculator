using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using MyobPayslipCalculator.Services;
using MyobPayslipCalculator.Model.Interfaces;
using MyobPayslipCalculator.Services.Interfaces;
using Ninject;

namespace MyobPayslipCalculator
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private static IPaySlipService _paySlipService;
        private static IFileService _fileService;

        static void Main()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _paySlipService = kernel.Get<IPaySlipService>();
            _fileService = kernel.Get<IFileService>();

            LogService.Welcome(Constants.WelcomeMessage);
            LogService.Info(Constants.EnterEmployeeFilePathMessage);

            var employeesFilePath = GetFilePath(true);
            var employees = GetEmployees(employeesFilePath);

            if (!employees.Any())
            {
                LogService.Error(Constants.PressAnyKeyToQuitMessage);
                Console.ReadKey();
                return;
            }

            var payslips = GetPaySlips(employees);
            LogService.Info(Constants.EnterPaySlipFilePathMessage);

            var payslipFilePath = GetFilePath(false);
            var payslipsPrinted = _fileService.ExportPaySlipsCSV(payslips, payslipFilePath);

            if (payslipsPrinted > 0)
            {
                LogService.Success(string.Format(Constants.PaySlipsSuccess, payslipsPrinted));
            }

            LogService.Info(Constants.PressAnyKeyToQuitMessage);
            Console.ReadKey();
        }


        private static string GetFilePath(bool forEmployeeFile)
        {
            var filePath = string.Empty;
            var validPath = false;

            while (!validPath)
            {
                filePath = Console.ReadLine();

                if (forEmployeeFile)
                {
                    if (!File.Exists(filePath))
                    {
                        LogService.Error(Constants.EmployeeFileNotExistsMessage);
                        continue;
                    }
                }
                else
                {
                    var directoryName = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryName))
                    {
                        LogService.Error(Constants.PaySlipFilePathInvalidMessage);
                        continue;
                    }
                }

                validPath = true;
            }

            return filePath;
        }

        private static IEnumerable<IEmployee> GetEmployees(string filePath)
        {
            var employees = _fileService.ImportEmployeesCSV(filePath);
            LogService.Info(string.Format(Constants.FoundEmployeesMessage, employees.Count()));

            return employees;
        }

        private static List<IPaySlip> GetPaySlips(IEnumerable<IEmployee> employees)
        {
            return employees.Select(employee => _paySlipService.CreatePaySlip(employee)).ToList();
        }

    }
}
