using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Infrastructure.TaxCalculator;

public class ForthLevelTaxCalculator : ITaxCalculator
{
    private const double ForthLevelStart = 70000;
    private const double ForthLevelEnd = 180000;
    private const double TaxRate = 0.33;

    public double CalculateTax(double income)
    {
        double taxableIncome = ForthLevelEnd - ForthLevelStart;
        if (income < ForthLevelEnd)
        {
            taxableIncome = income - ForthLevelStart;
            taxableIncome = taxableIncome < 0 ? 0 : taxableIncome;
        }

        return taxableIncome * TaxRate;
    }
}