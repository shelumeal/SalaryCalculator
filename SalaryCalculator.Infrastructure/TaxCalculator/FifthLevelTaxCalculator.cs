using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Infrastructure.TaxCalculator;

public class FifthLevelTaxCalculator : ITaxCalculator
{
    private const double FifthLevelStart = 180000;
    private const double TaxRate = 0.39;

    public double CalculateTax(double income)
    {
        double taxableIncome = income - FifthLevelStart;
        if (taxableIncome < 0)
        {
            return 0;
        }

        return taxableIncome * TaxRate;
    }
}