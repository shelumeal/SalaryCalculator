using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Infrastructure.TaxCalculator;

public class FirstLevelTaxCalculator : ITaxCalculator
{
    private const double FirstLevelEnd = 14000;
    private const double TaxRate = 0.105;
    
    public double CalculateTax(double income)
    {
        double taxableIncome = income > FirstLevelEnd ? FirstLevelEnd : income;
        return taxableIncome * TaxRate;
    }
}