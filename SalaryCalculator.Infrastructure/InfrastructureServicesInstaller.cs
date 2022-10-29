using Microsoft.Extensions.DependencyInjection;
using SalaryCalculator.Application.ImportExport;
using SalaryCalculator.Application.SalaryManager;
using SalaryCalculator.Infrastructure.TaxCalculator;

namespace SalaryCalculator.Infrastructure;

public static class InfrastructureServicesInstaller
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICsvImporter, CsvImporter>();
        services.AddScoped<ITaxCalculator, FirstLevelTaxCalculator>();
        services.AddScoped<ITaxCalculator, SecondLevelTaxCalculator>();
        services.AddScoped<ITaxCalculator, ThirdLevelTaxCalculator>();
        services.AddScoped<ITaxCalculator, ForthLevelTaxCalculator>();
        services.AddScoped<ITaxCalculator, FifthLevelTaxCalculator>();
        return services;
    }
}