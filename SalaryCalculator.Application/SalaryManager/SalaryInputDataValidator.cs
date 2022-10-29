using FluentValidation;

namespace SalaryCalculator.Application.SalaryManager;

public class SalaryInputDataValidator : AbstractValidator<SalaryMetaDto>
{
    public SalaryInputDataValidator()
    {
        RuleFor(d => d.FirstName).NotNull().NotEmpty().WithMessage("First Name is required");
        RuleFor(d => d.LastName).NotNull().NotEmpty().WithMessage("Last Name is required");
        RuleFor(d => d.PayPeriod).NotNull().NotEmpty().WithMessage("Pay period is required");
        RuleFor(d => d.AnnualSalary).GreaterThan(0);
        RuleFor(d => d).Custom(
            (data, context) =>
            {
                if (!data.IsValidPayPeriod)
                {
                    context.AddFailure("PayPeriod", $"Invalid pay period value. You have entered {data.PayPeriod} for {data.FirstName} {data.LastName}");
                }
                if (data.SuperRate > 50 || data.SuperRate < 0)
                {
                    context.AddFailure("SuperRate", $"Super rate should be between 0 and 50 inclusive. You have entered {data.SuperRate} for {data.FirstName} {data.LastName}");
                }
            }
            ).When(c => !string.IsNullOrWhiteSpace(c.PayPeriod));
        
    }
}