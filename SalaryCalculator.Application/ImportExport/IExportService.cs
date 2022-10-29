namespace SalaryCalculator.Application.ImportExport;

public interface IExportService
{
    (Stream StreamToExport, List<string> Errors) ExportSalaryData(Stream fileData);
}