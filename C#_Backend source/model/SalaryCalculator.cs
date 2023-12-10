using Backend.Interfaces;
using System;

namespace Backend.Model
{
    public class SalaryCalculator : ISalaryCalculator
    {
        private const double TaxThreshold = 1000;
        private const double TaxRate = 0.10;
        private const double SocialContributionRate = 0.15;
        private const double SocialContributionCeiling = 3000;

        private readonly ILogger _logger;

        public SalaryCalculator(ILogger logger)
        {
            _logger = logger;
        }

        public double CalculateNetSalary(double grossSalary)
        {
            try
            {
                // If gross salary is smaller or equal to the tax threshold
                // there is not taxation and social contributions
                // and we just return the price
                if (grossSalary <= TaxThreshold)
                {
                    return grossSalary;
                }

                double incomeTax = grossSalary * TaxRate;
                grossSalary -= incomeTax;

                // If gross salary is not bigger than 3000
                // we have 15% social contributions
                double socialContributes = 0;
                if (grossSalary <= SocialContributionCeiling)
                {
                    socialContributes = grossSalary * SocialContributionRate;
                    grossSalary -= socialContributes;

                    return grossSalary;
                }

                return grossSalary;
            }
            catch (Exception ex)
            {
                _logger.Error("Error calculating net salary", ex);
                throw;
            }
        }
    }
}
