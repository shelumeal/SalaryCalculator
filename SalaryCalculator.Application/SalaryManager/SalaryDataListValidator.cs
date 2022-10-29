namespace SalaryCalculator.Application.SalaryManager;

public class SalaryDataListValidator
{
    private readonly IReadOnlyList<SalaryMetaDto> SalaryMetaData;
    private SalaryInputDataValidator validator;
    public SalaryDataListValidator(IReadOnlyList<SalaryMetaDto> salaryData)
    {
        this.SalaryMetaData = salaryData;
        this.validator = new SalaryInputDataValidator();
    }

    public (List<string> Errors, List<SalaryMetaDto> ValidItems) Validate()
    {
        var validItems = new List<SalaryMetaDto>();
        List<string> errors = new List<string>();
        foreach (var salary in SalaryMetaData)
        {
            var validationResult = this.validator.Validate(salary);
            if (validationResult.IsValid)
            {
                validItems.Add(salary);
            }
            else
            {
                errors.AddRange(validationResult.Errors.Select(f => f.ErrorMessage).ToList());
            }
        }

        return (errors, validItems);
    }
}