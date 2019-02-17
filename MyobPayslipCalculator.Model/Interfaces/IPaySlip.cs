namespace MyobPayslipCalculator.Model.Interfaces
{
    public interface IPaySlip
    {
        string EmployeeName { get; set; }
        decimal GrossIncome { get; set; }
        decimal IncomeTax { get; set; }
        string PayPeriod { get; set; }
        decimal NetIncome { get; }
        decimal SuperAmount { get; set; }
        string[] ToPrint();
    }
}
