using Microsoft.Extensions.DependencyInjection;
using SalaryCalculator.Application.ImportExport;
using SalaryCalculator.Application.SalaryManager;

namespace SalaryCalculator.Application;

public static class ApplicationServiceInstaller
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMonthlySalaryCalculator, MonthlySalaryCalculator>();
        services.AddScoped<IExportService, ExportService>();
        return services;
    }
}