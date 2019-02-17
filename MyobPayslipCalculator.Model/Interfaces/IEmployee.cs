using System;

namespace MyobPayslipCalculator.Model.Interfaces
{
    public interface IEmployee
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; }
        int AnnualSalary { get; set; }
        decimal SuperRate { get; set; }
        string PayPeriod { get; set; }
    }
}
