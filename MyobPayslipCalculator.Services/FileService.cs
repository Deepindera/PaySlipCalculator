using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MyobPayslipCalculator.Model;
using MyobPayslipCalculator.Model.Interfaces;
using MyobPayslipCalculator.Services.Interfaces;

namespace MyobPayslipCalculator.Services
{
    public class FileService: IFileService
    {
        private const string PERCENTSIGN = "%";
        private const char DELIMITER = ',';      

        public IEnumerable<IEmployee> ImportEmployeesCSV(string filePath)
        {
            try
            {
                var employeeFile = File.ReadAllLines(filePath);

                var employees = from data in employeeFile
                                let employee = data.Split(DELIMITER)
                                select new Employee
                                {
                                    FirstName = employee[0],
                                    LastName = employee[1],
                                    AnnualSalary = int.Parse(employee[2]),
                                    SuperRate = decimal.Parse(employee[3].Replace(PERCENTSIGN, string.Empty)),
                                    PayPeriod = employee[4],
                                };

                return employees;

            }
            catch (Exception e)
            {
                LogService.Error(e.Message);
                throw;
            }           
        }

        //Returns the number of lines printed to the csv file
        public int ExportPaySlipsCSV(List<IPaySlip> paySlips, string filePath)
        {
            if (!paySlips.Any())
            {
                LogService.Error(Constants.NoDataToWrite);
                return 0;
            }

            var sb = new StringBuilder();
            foreach (var payslip in paySlips)
            {
                sb.AppendLine(string.Join(DELIMITER.ToString(), payslip.ToPrint()));
            }

            try
            {
                File.WriteAllText(filePath, sb.ToString());
                return Regex.Matches(sb.ToString(), Environment.NewLine).Count;
            }
            catch (Exception e)
            {
                LogService.Error(e.Message);
                throw;
            }            
        }
    }
}
