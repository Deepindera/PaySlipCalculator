using MyobPayslipCalculator.Model;
using MyobPayslipCalculator.Model.Interfaces;
using MyobPayslipCalculator.Services.Interfaces;

namespace MyobPayslipCalculator.Services
{
    public class PaySlipService : IPaySlipService
    {
        private readonly IRoundingService _roundingService;
        private readonly ITaxCalculatorService _taxCalculatorService;        

        public PaySlipService(IRoundingService roundingService, ITaxCalculatorService taxCalculatorService)
        {
            _roundingService = roundingService;
            _taxCalculatorService = taxCalculatorService;            
        }

        public IPaySlip CreatePaySlip(IEmployee employee)
        {
            var paySlip = new PaySlip
            {
                PayPeriod = employee.PayPeriod,
                EmployeeName = employee.FullName
            };

            var unRoundedGrossIncome = CalculateGrossIncome(employee.AnnualSalary);
            paySlip.GrossIncome = _roundingService.ApplyRounding(unRoundedGrossIncome);

            var unRoundedIncomeTax = _taxCalculatorService.CalculateTax(employee.AnnualSalary);
            paySlip.IncomeTax = _roundingService.ApplyRounding(unRoundedIncomeTax);

            var unRoundedSuper = CalculateSuper(paySlip.GrossIncome, employee.SuperRate);
            paySlip.SuperAmount = _roundingService.ApplyRounding(unRoundedSuper);

            return paySlip;
        }

        private decimal CalculateGrossIncome(int salary)
        {
            return salary / 12;
        }

        private decimal CalculateSuper(decimal income, decimal super)
        {
            return income * super / 100;
        }
    }
}
