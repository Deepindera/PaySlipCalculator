using System.Diagnostics.CodeAnalysis;
using MyobPayslipCalculator.Model;
using MyobPayslipCalculator.Services;
using NUnit.Framework;

namespace MyobPayslipCalculator.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PaySlipServiceTests
    {
      
        [Test]
        public void Create_PaySlip_Generates_Expected_Results_For_An_Employee()
        {
            //Arrange
            var employee = new Employee
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                AnnualSalary = 60050,
                SuperRate = 9,
                PayPeriod = "01 March - 31 March"
            };

            var roundingService = new RoundingService();           
            var taxCalculatorService = new TaxCalculatorService();
            var payslipService = new PaySlipService(roundingService, taxCalculatorService);

            //Act
            var result = payslipService.CreatePaySlip(employee);

            //Assert
            Assert.AreEqual(employee.FullName, result.EmployeeName, "Employee Name incorrect");
            Assert.AreEqual(employee.PayPeriod, result.PayPeriod, "Pay Period incorrect");
            Assert.AreEqual(5004, result.GrossIncome, "Gross Income incorrect");
            Assert.AreEqual(922, result.IncomeTax, "Income Tax incorrect");
            Assert.AreEqual(4082, result.NetIncome, "Net Income incorrect");
            Assert.AreEqual(450, result.SuperAmount, "Super incorrect");
        }
    }
}
