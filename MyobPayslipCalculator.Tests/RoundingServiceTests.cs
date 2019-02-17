using System.Diagnostics.CodeAnalysis;
using MyobPayslipCalculator.Services;
using MyobPayslipCalculator.Services.Interfaces;
using NUnit.Framework;

namespace MyobPayslipCalculator.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class RoundingServiceTests
    {
        [TestCase(60000.49, 60000)]
        [TestCase(60000.01, 60000)]
        [TestCase(60000.25, 60000)]
        public void When_ApplyRounding_Called_With_Less_Than_50_Cents_Then_Round_Down_Is_Applied(decimal moneyToRound, decimal roundedResult)
        {
            //Arrange
            var roundingService = new RoundingService();

            //Apply
            var result = roundingService.ApplyRounding(moneyToRound);

            //Assert
            Assert.AreEqual(roundedResult,result);

        }

        [TestCase(60000.50, 60001)]
        [TestCase(60000.51, 60001)]
        [TestCase(60000.99, 60001)]
        public void When_ApplyRounding_Called_With_EqualOrMore_Than_50_Cents_Then_Round_Up_Is_Applied(decimal moneyToRound, decimal roundedResult)
        {
            //Arrange
            var roundingService = new RoundingService();

            //Apply
            var result = roundingService.ApplyRounding(moneyToRound);

            //Assert
            Assert.AreEqual(roundedResult, result);

        }
    }
}
