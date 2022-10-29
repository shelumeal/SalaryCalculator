using System.Globalization;
using CsvHelper;
using SalaryCalculator.Application.ImportExport;

namespace SalaryCalculator.Infrastructure;

public class CsvImporter : ICsvImporter
{
    
    public IReadOnlyList<T> GetCsvData<T>(Stream dataStream)
    {
        using var reader = new StreamReader(dataStream);
        using var csvReader = new CsvReader(reader, new(CultureInfo.InvariantCulture) { MissingFieldFound = null, PrepareHeaderForMatch = args => args.Header.ToLower().Trim() });

        return csvReader.GetRecords<T>().ToList();
    }

    public Stream GetCsvFile<TOut>(IReadOnlyList<TOut> dataToExport)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(dataToExport);
        csvWriter.Flush();
        memoryStream.Position = 0;
        return new MemoryStream(memoryStream.ToArray());
    }
}