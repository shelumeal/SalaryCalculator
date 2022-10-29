namespace SalaryCalculator.Application.SalaryManager;

public class SalaryExportDto
{
    public string Name { get; set; }
    
    public string PayPeriod { get; set; }

    public double GrossIncome { get; set; }
    
    public double IncomeTax { get; set; }
    
    public double NetIncome { get; set; }
    
    public double Super { get; set; }
}