using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using MyobPayslipCalculator.Model;
using MyobPayslipCalculator.Model.Interfaces;
using MyobPayslipCalculator.Services;
using NUnit.Framework;

namespace MyobPayslipCalculator.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class FileServiceTests
    {
        private const string EmployeeFileName = "test_employeesdata.csv";
        private const string ResourcesDirectory = @"Resources\";
        private const string BinDirectory = "bin";        
        private const string CSVExtension = ".csv";

        [Test]
        public void When_ImportEmployeesCSV_Called_Returns_List_Of_Employees()
        {
            //Arrange
            var filePath = GetResourcesFolder() + EmployeeFileName;          
            var fileService = new FileService();
            
            
            //Act
            var result = fileService.ImportEmployeesCSV(filePath);

            //Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(r => r.FirstName == "David" && r.LastName == "Rudd" && r.AnnualSalary == 60050 && r.SuperRate == 9 && r.PayPeriod == "01 March – 31 March"), "Employee 1 not match");
            Assert.IsTrue(result.Any(r => r.FirstName == "Ryan" && r.LastName == "Chen" && r.AnnualSalary == 120000 && r.SuperRate == 10 && r.PayPeriod == "01 March – 31 March"), "Employee 2 not match");

        }

        [Test]
        public void When_ImportEmployeesCSV_Called_With_Wrong_FilePath_It_Throws_Exception()
        {
            //Arrange
            var filePath = GetResourcesFolder() + "Incorrect File Path";            
            var fileService = new FileService();


            //Act and Assert

            var ex = Assert.Throws<FileNotFoundException>(() => fileService.ImportEmployeesCSV(filePath));
            Assert.That(ex.Message, Does.Contain("Could not find file"));

        }

        [Test]
        public void When_ExportPaySlipsCSV_Called_With_Wrong_FilePath_It_Throws_Exception()
        {
            //Arrange
            var filePath = Path.GetTempPath()+@"WrongPath\";
            var fileName = filePath + Guid.NewGuid() + CSVExtension;
            var fileService = new FileService();
            var payslips = GetPaySlips();
                      
            //Act and Assert
            var ex = Assert.Throws<DirectoryNotFoundException>(() => fileService.ExportPaySlipsCSV(payslips, fileName));
            Assert.That(ex.Message, Does.Contain("Could not find a part of the path"));

        }

        [Test]
        public void When_ExportPaySlipsCSV_Called_Writes_Correct_PaySlips_Data_To_CSV()
        {
            //Arrange
            var filePath = Path.GetTempPath();
            var fileName = filePath + Guid.NewGuid() + CSVExtension;            
            var fileService = new FileService();
            var payslips = GetPaySlips();

            //Act
            var result = fileService.ExportPaySlipsCSV(payslips, fileName);
            var readPaySlipsData = ReadPaySlipsCSV(fileName);

            //Assert
            Assert.AreEqual(2, result, "PaySlips count not match");

            Assert.AreEqual(readPaySlipsData.First().EmployeeName, payslips.First().EmployeeName, "Employee 1 Names not match");
            Assert.AreEqual(readPaySlipsData.First().GrossIncome, payslips.First().GrossIncome, "Employee 1 GrossIncome not match");
            Assert.AreEqual(readPaySlipsData.First().IncomeTax, payslips.First().IncomeTax, "Employee 1 IncomeTax not match");
            Assert.AreEqual(readPaySlipsData.First().PayPeriod, payslips.First().PayPeriod, "Employee 1 PayPeriod not match");
            Assert.AreEqual(readPaySlipsData.First().SuperAmount, payslips.First().SuperAmount, "Employee 1 SuperAmount not match");

            Assert.AreEqual(readPaySlipsData.Last().EmployeeName, payslips.Last().EmployeeName,"Employee 2 Names not match");
            Assert.AreEqual(readPaySlipsData.Last().GrossIncome, payslips.Last().GrossIncome, "Employee 2 GrossIncome not match");
            Assert.AreEqual(readPaySlipsData.Last().IncomeTax, payslips.Last().IncomeTax, "Employee 2 IncomeTax not match");
            Assert.AreEqual(readPaySlipsData.Last().PayPeriod, payslips.Last().PayPeriod, "Employee 2 PayPeriod not match");
            Assert.AreEqual(readPaySlipsData.Last().SuperAmount, payslips.Last().SuperAmount, "Employee 2 SuperAmount not match");            
        }

        [Test]
        public void When_ExportPaySlipsCSV_Called_With_No_PaySlip_Data_Then_It_Returns_Zero_Count_Of_PaySlips()
        {
            //Arrange
            var filePath = Path.GetTempPath();
            var fileName = filePath + Guid.NewGuid() + CSVExtension;           
            var fileService = new FileService();
            var payslips = new List<IPaySlip>();

            //Act
            var result = fileService.ExportPaySlipsCSV(payslips, fileName);

            //Assert
            Assert.AreEqual(0, result, "Payslip count must be 0");

        }

        private static List<IPaySlip> GetPaySlips()
        {
            var paySlipOne = new PaySlip
            {
                EmployeeName = "Test One",
                PayPeriod = "1 March - 31 March",
                SuperAmount = 401,
                GrossIncome = 5001,
                IncomeTax = 901
            };

            var paySlipTwo = new PaySlip
            {
                EmployeeName = "Test Two",
                PayPeriod = "1 March - 31 March",
                SuperAmount = 402,
                GrossIncome = 5002,
                IncomeTax = 902
            };

            var paySlips = new List<IPaySlip> { paySlipOne, paySlipTwo };
            return paySlips;
        }


        private string GetResourcesFolder()
        {
            var projectPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf(BinDirectory)) + ResourcesDirectory;
            return projectPath;
        }       

        private IEnumerable<IPaySlip> ReadPaySlipsCSV(string filePath)
        {
            var payslipfile = File.ReadAllLines(filePath);

            var payslips = from data in payslipfile
                           let pay = data.Split(',')
                           select new PaySlip
                           {
                               EmployeeName = pay[0],
                               PayPeriod = pay[1],
                               GrossIncome = decimal.Parse(pay[2]),
                               IncomeTax = decimal.Parse(pay[3]),
                               SuperAmount = decimal.Parse(pay[5]),

                           };

            return payslips;

        }
    }
}
