using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Application.ImportExport;
using SalaryCalculator.Web.Models;

namespace SalaryCalculator.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IExportService exportService;
    

    public HomeController(ILogger<HomeController> logger, IExportService exportService)
    {
        _logger = logger;
        this.exportService = exportService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public ActionResult DownloadSalaryList(IFormFile? file)
    {
        TempData.Clear();

        if (file != null)
        {
            var stream = OpenAndCopy(file);
            var result = this.exportService.ExportSalaryData(stream);
            if (result.Errors.Count > 0)
            {
                this._logger.LogWarning(string.Join(",", result.Errors));
            }
            return this.File(result.StreamToExport, "text/csv", $"{DateTime.Now.ToLongDateString()}_salary_slip.csv");
        }
        else
        {
            this._logger.LogWarning("File is empty");
            TempData["messageError"] = "File cannot be empty. Please select a file";
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    [NonAction]
    private static Stream OpenAndCopy(IFormFile? formFile)
    {
        if (formFile == null)
        {
            return Stream.Null;
        }

        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }
}