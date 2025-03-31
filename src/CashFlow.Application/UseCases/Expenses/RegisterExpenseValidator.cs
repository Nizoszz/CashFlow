using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;
namespace CashFlow.Application.UseCases.Expenses
{
    public class RegisterExpenseValidator : AbstractValidator<RequestExpenseJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty().WithMessage(ResourceErrorMessages.REQUIRED_TITLE);
            RuleFor(request => request.Amount)
                .GreaterThan(0).WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
            RuleFor(request => request.Date)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
            RuleFor(request => request.PaymentType)
                .IsInEnum().WithMessage(ResourceErrorMessages.INVALID_PAYMENT_TYPE);
        }
    }
}
