using CashFlow.Communication.Requests;
using FluentValidation;
namespace CashFlow.Application.UseCases.Expenses
{
    class RegisterExpenseValidator : AbstractValidator<RequestExpenseJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(request => request.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than or equal to zero.");
            RuleFor(request => request.Date)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Date must be in the future.");
            RuleFor(request => request.PaymentType)
                .IsInEnum().WithMessage("Payment type is not valid.");
        }
    }
}
