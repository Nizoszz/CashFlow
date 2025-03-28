using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;

namespace CashFlow.Application.UseCases.Expenses
{
    public class RegisterExpensesUseCase
    {
        public ResponseExpenseJson Execute(RequestExpenseJson request)
        {
            return new ResponseExpenseJson();
        }
        private void Validate(RequestExpenseJson request)
        {
            var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
            if (titleIsEmpty)
            {
                throw new ArgumentException("Title is required");
            }
            var amountIsNegative = request.Amount < 0;
            if (amountIsNegative)
            {
                throw new ArgumentException("Amount must be greater than or equal to zero");
            }
            var dateIsValid = DateTime.Compare(request.Date, DateTime.UtcNow);
            if (dateIsValid > 0)
            {
                throw new ArgumentException("Date must be in the future");
            }
            var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
            if (!paymentTypeIsValid)
            {
                throw new ArgumentException("Payment type is not valid.");
            }
        }
    }
}
