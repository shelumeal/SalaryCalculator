using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Application.ImportExport;

public class ExportService : IExportService
{
    private readonly IMonthlySalaryCalculator monthlySalaryCalculator;
    private readonly ICsvImporter csvImporter;
    
    public ExportService(IMonthlySalaryCalculator monthlySalaryCalculator, ICsvImporter csvImporter)
    {
        this.monthlySalaryCalculator = monthlySalaryCalculator;
        this.csvImporter = csvImporter;
    }

    public (Stream StreamToExport, List<string> Errors) ExportSalaryData(Stream fileData)
    {
        var importResult = this.monthlySalaryCalculator.GetSalaryOrErrors(fileData);
        var stram = this.csvImporter.GetCsvFile(importResult.ListToExport);
        return (stram, importResult.Errors);
    }
}