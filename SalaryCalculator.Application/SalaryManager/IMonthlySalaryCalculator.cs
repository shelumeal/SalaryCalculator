namespace SalaryCalculator.Application.SalaryManager;

public interface IMonthlySalaryCalculator
{
    (IReadOnlyList<SalaryExportDto> ListToExport, List<string> Errors) GetSalaryOrErrors(Stream fileData);
}