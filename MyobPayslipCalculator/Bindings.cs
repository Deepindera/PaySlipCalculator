using MyobPayslipCalculator.Services;
using MyobPayslipCalculator.Services.Interfaces;
using Ninject.Modules;

namespace MyobPayslipCalculator
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IPaySlipService>().To<PaySlipService>();
            Bind<IRoundingService>().To<RoundingService>();
            Bind<ITaxCalculatorService>().To<TaxCalculatorService>();
            Bind<IFileService>().To<FileService>();
        }
    }
}
