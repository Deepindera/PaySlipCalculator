namespace MyobPayslipCalculator.Model
{
    public class TaxTable
    {
        public int LowerLimit { get; set; }
        public long UpperLimit { get; set; }
        public decimal FactorInCents { get; set; }
        public decimal BaseAmount { get; set; }
        public bool Taxable { get; set; }
    }
}
