using System;
using MyobPayslipCalculator.Model.Interfaces;

namespace MyobPayslipCalculator.Model
{
    public class Employee :IEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
        public string PayPeriod { get; set; }
    }
}
