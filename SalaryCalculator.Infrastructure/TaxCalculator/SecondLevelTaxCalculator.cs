using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Infrastructure.TaxCalculator;

public class SecondLevelTaxCalculator : ITaxCalculator
{
    private const double SecondLevelStart = 14000;
    private const double SecondLevelEnd = 48000;
    private const double TaxRate = 0.175;
    
    public double CalculateTax(double income)
    {
        double taxableIncome = SecondLevelEnd - SecondLevelStart;
        if (income < SecondLevelEnd)
        {
            taxableIncome = income - SecondLevelStart;
            taxableIncome = taxableIncome < 0 ? 0 : taxableIncome;
        }
        return taxableIncome * TaxRate;
    }
}