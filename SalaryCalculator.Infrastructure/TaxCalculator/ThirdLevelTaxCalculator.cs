using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Infrastructure.TaxCalculator;

public class ThirdLevelTaxCalculator : ITaxCalculator
{
    private const double ThirdLevelStart = 48000;
    private const double ThirdLevelEnd = 70000;
    private const double TaxRate = 0.3;
    
    public double CalculateTax(double income)
    {
        double taxableIncome = ThirdLevelEnd - ThirdLevelStart;
        if (income < ThirdLevelEnd)
        {
            taxableIncome = income - ThirdLevelStart;
            taxableIncome = taxableIncome < 0 ? 0 : taxableIncome;
        }
        return taxableIncome * TaxRate;
    }
}