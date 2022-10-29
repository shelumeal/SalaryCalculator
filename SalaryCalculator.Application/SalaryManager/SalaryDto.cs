namespace SalaryCalculator.Application.SalaryManager;

public class SalaryMetaDto
{
    private Dictionary<string, int> monthNumbers = new Dictionary<string, int>()
    {
        {"January", 1}, {"February", 2}, {"March", 3}, {"April", 4}, {"May", 5}, {"June", 6}, {"July", 7},
        {"August", 8}, {"September", 9}, {"October", 10}, {"November", 11}, {"December", 12}
    };
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int AnnualSalary { get; set; }
    
    public double SuperRate { get; set; }
    
    public string PayPeriod { get; set; }

    public int MonthNumber => this.monthNumbers[this.PayPeriod];

    public bool IsValidPayPeriod => this.monthNumbers.ContainsKey(this.PayPeriod);

    public string PayPeriodValue
    {
        get
        {
            DateTime date = new DateTime(1900, MonthNumber, 01);
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var lastDay = date.AddMonths(1).AddDays(-1);
            return $"{firstDay:dd MMMM} -  {lastDay:dd MMMM}";
        }
    }
}