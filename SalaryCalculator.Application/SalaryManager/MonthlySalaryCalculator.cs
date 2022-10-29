using System.ComponentModel.DataAnnotations;
using SalaryCalculator.Application.ImportExport;

namespace SalaryCalculator.Application.SalaryManager;

public class MonthlySalaryCalculator : IMonthlySalaryCalculator
{
    private readonly ICsvImporter csvImporter;
    private readonly IEnumerable<ITaxCalculator> taxCalculators;

    public MonthlySalaryCalculator(ICsvImporter csvImporter, IEnumerable<ITaxCalculator> taxCalculators)
    {
        this.csvImporter = csvImporter;
        this.taxCalculators = taxCalculators;
    }

    public (IReadOnlyList<SalaryExportDto> ListToExport, List<string> Errors) GetSalaryOrErrors(Stream fileData)
    {
        if (fileData == null)
        {
            throw new ValidationException("File data is not valid");
        }

        var salaryData = this.csvImporter.GetCsvData<SalaryMetaDto>(fileData);
        if (salaryData.Count == 0)
        {
            throw new ValidationException("File has no data");
        }

        var validator = new SalaryDataListValidator(salaryData);
        var validDataOrErrors = validator.Validate();
        var exportItems = this.GetCalculatedSalary(validDataOrErrors.ValidItems);
        return (exportItems, validDataOrErrors.Errors);
    }

    private IReadOnlyList<SalaryExportDto> GetCalculatedSalary(IReadOnlyList<SalaryMetaDto> validItems)
    {
        List<SalaryExportDto> result = new List<SalaryExportDto>();
        foreach (var item in validItems)
        {
            double grossIncome = Math.Round(((double)item.AnnualSalary / 12),2);
            double tax = 0;
            foreach (var calculator in this.taxCalculators)
            {
                tax += calculator.CalculateTax(item.AnnualSalary);
            }
            tax = Math.Round((tax / 12),2);
            double netSalary =  grossIncome - tax;
            double super = grossIncome * (item.SuperRate / 100);
            var exportItem = new SalaryExportDto
            {
                Name = $"{item.FirstName} {item.LastName}",
                PayPeriod = item.PayPeriodValue,
                GrossIncome = Math.Round(grossIncome, 2),
                IncomeTax = tax,
                NetIncome = Math.Round(netSalary, 2),
                Super = Math.Round(super, 2),
            };
            result.Add(exportItem);
        }

        return result;
    }
}