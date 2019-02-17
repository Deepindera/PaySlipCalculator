using MyobPayslipCalculator.Model.Interfaces;

namespace MyobPayslipCalculator.Services.Interfaces
{
    public interface IPaySlipService
    {
        IPaySlip CreatePaySlip(IEmployee employee);
    }
}
