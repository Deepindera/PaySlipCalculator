using MyobPayslipCalculator.Model.Interfaces;

namespace MyobPayslipCalculator.Model
{
    public class PaySlip : IPaySlip
    {
        public string EmployeeName { get; set; }
        public string PayPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal SuperAmount { get; set; }
        public decimal NetIncome => GrossIncome - IncomeTax;


        public string[] ToPrint()
        {
            return new[]
            {
                EmployeeName, PayPeriod, GrossIncome.ToString("#.##"), IncomeTax.ToString("#.##"), NetIncome.ToString("#.##"), SuperAmount.ToString("#.##")
            };
        }

    }
}
