namespace SalaryCalculator.Application.ImportExport;

public interface ICsvImporter
{
    IReadOnlyList<T> GetCsvData<T>(Stream dataStream);
    
    Stream GetCsvFile<TOut>(IReadOnlyList<TOut> dataToExport);
}