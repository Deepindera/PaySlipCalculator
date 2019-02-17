using System.Collections.Generic;
using MyobPayslipCalculator.Model.Interfaces;

namespace MyobPayslipCalculator.Services.Interfaces
{
    public interface IFileService
    {
        IEnumerable<IEmployee> ImportEmployeesCSV(string filePath);
        int ExportPaySlipsCSV(List<IPaySlip> paySlips, string filePath);
    }
}
