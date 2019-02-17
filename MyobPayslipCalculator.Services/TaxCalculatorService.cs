using System.Collections.Generic;
using System.Linq;
using MyobPayslipCalculator.Model;
using MyobPayslipCalculator.Services.Interfaces;
using MyobPayslipCalculator.Services.Properties;
using Newtonsoft.Json;

namespace MyobPayslipCalculator.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        public decimal CalculateTax(int salary)
        {
            var taxTable = JsonConvert.DeserializeObject<List<TaxTable>>(Resources.taxtable).OrderBy(t => t.LowerLimit);
            var taxBracket = taxTable.Single(t => salary > t.LowerLimit && salary <= t.UpperLimit);
            var tax = CalculateTaxForBracket(salary, taxBracket);

            return tax;
        }

        private decimal CalculateTaxForBracket(int salary, TaxTable taxbracket)
        {
            var dollarAmount = taxbracket.FactorInCents / 100;            
            var tax = taxbracket.Taxable ? (taxbracket.BaseAmount + (salary - taxbracket.LowerLimit) * dollarAmount) / 12 : 0;
            return tax;
        }
    }
}

