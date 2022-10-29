using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using SalaryCalculator.Application.ImportExport;
using SalaryCalculator.Application.SalaryManager;
using SalaryCalculator.Infrastructure.TaxCalculator;

namespace SalaryCalculator.Tests;

public class SalaryCalculatorTests
{
    private readonly Mock<ICsvImporter> mCsvImporter = new();
    private IMonthlySalaryCalculator calculator;

    [SetUp]
    public void SetUp()
    {
        var calculatorList = new List<ITaxCalculator>()
        {
            new FirstLevelTaxCalculator(),
            new SecondLevelTaxCalculator(),
            new ThirdLevelTaxCalculator(),
            new ForthLevelTaxCalculator(),
            new FifthLevelTaxCalculator(),
        };
        this.calculator = new MonthlySalaryCalculator(this.mCsvImporter.Object, calculatorList);
    }
    
    [Test]
    public void For_Given_Salary_Tax_Is_Correct()
    {
        this.mCsvImporter.Setup(m => m.GetCsvData<SalaryMetaDto>(It.IsAny<Stream>())).Returns(GetValidSalaryData());
        var result = this.calculator.GetSalaryOrErrors(Stream.Null);
        Assert.IsTrue(result.Errors.Count == 0);
        Assert.IsTrue(result.ListToExport.Count ==1);
        Assert.IsTrue(Math.Abs(result.ListToExport[0].IncomeTax - 919.58) < 0.001);
        Assert.IsTrue(Math.Abs(result.ListToExport[0].GrossIncome - 5004.17) < 0.001);
        Assert.IsTrue(Math.Abs(result.ListToExport[0].NetIncome - 4084.59) < 0.001);
        Assert.IsTrue(Math.Abs(result.ListToExport[0].Super - 450.38) < 0.001);
    }

    [Test]
    public void Given_Data_Has_A_Invalid_Month()
    {
        this.mCsvImporter.Setup(m => m.GetCsvData<SalaryMetaDto>(It.IsAny<Stream>())).Returns(GetInvalidSalaryData());
        var result = this.calculator.GetSalaryOrErrors(Stream.Null);
        Assert.IsTrue(result.Errors.Count > 0);
        Assert.IsTrue(result.ListToExport.Count == 0);
        Assert.IsTrue(result.Errors[0].Contains("Incorrect"));
    }

    [Test]
    public void Given_Data_Has_A_Invalid_Super_Rate()
    {
        this.mCsvImporter.Setup(m => m.GetCsvData<SalaryMetaDto>(It.IsAny<Stream>())).Returns(GetInvalidSalarySuperRateData());
        var result = this.calculator.GetSalaryOrErrors(Stream.Null);
        Assert.IsTrue(result.Errors.Count > 0);
        Assert.IsTrue(result.ListToExport.Count == 0);
        Assert.IsTrue(result.Errors[0].Contains("Super rate should be"));
    }
    private List<SalaryMetaDto> GetValidSalaryData()
    {
        return new List<SalaryMetaDto>
        {
            new SalaryMetaDto()
            {
                FirstName = "John",
                LastName = "Smith",
                AnnualSalary = 60050,
                SuperRate = 9,
                PayPeriod = "March"
            }
        };
    }
    
    private List<SalaryMetaDto> GetInvalidSalaryData()
    {
        return new List<SalaryMetaDto>
        {
            new SalaryMetaDto()
            {
                FirstName = "John",
                LastName = "Smith",
                AnnualSalary = 60050,
                SuperRate = 9,
                PayPeriod = "Incorrect"
            }
        };
    }
    
    private List<SalaryMetaDto> GetInvalidSalarySuperRateData()
    {
        return new List<SalaryMetaDto>
        {
            new SalaryMetaDto()
            {
                FirstName = "John",
                LastName = "Smith",
                AnnualSalary = 60050,
                SuperRate = 55,
                PayPeriod = "March"
            }
        };
    }
} 