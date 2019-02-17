using System;
using System.Diagnostics.CodeAnalysis;
using MyobPayslipCalculator.Services;
using NUnit.Framework;

namespace MyobPayslipCalculator.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class TaxCalculatorServiceTests
    {       
        [TestCase(18200, 0)]
        [TestCase(25000, 107.666)]
        [TestCase(60050, 921.9375)]
        [TestCase(87050, 1653.375)]        
        [TestCase(180000, 4519.333)]        
        public void When_CalculateTax_Called_With_Given_Paratemeters_Returns_Correct_Tax_Results(int salary, decimal expectedTax)
        {
            //Arrange
            var taxCalculatorService = new TaxCalculatorService();

            //Act
            var result = taxCalculatorService.CalculateTax(salary);

            //Assert
            Assert.AreEqual(decimal.Round(expectedTax, 2, MidpointRounding.AwayFromZero), decimal.Round(result, 2, MidpointRounding.AwayFromZero));

        }
    }
}
