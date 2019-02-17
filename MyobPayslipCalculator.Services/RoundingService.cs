using System;
using MyobPayslipCalculator.Services.Interfaces;

namespace MyobPayslipCalculator.Services
{
    public class RoundingService : IRoundingService
    {
        //Rounded down and up based on 50 cents threshold
        public int ApplyRounding(decimal unroundedvalue)
        {
            var integral = (int) Math.Truncate(unroundedvalue);
            var fraction = unroundedvalue - integral;

            return fraction >= 0.50M ? integral + 1 : integral;

        }
    }
}
