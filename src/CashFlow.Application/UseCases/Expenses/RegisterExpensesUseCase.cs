using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses
{
    public class RegisterExpensesUseCase : IRegisterExpensesUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        public RegisterExpensesUseCase(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }
        public ResponseExpenseJson Execute(RequestExpenseJson request)
        {
            Validate(request);
            var entity = new ExpenseEntity
            {
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                Title = request.Title,
                PaymentType = (PaymentType)request.PaymentType,

            };
            _expensesRepository.Add(entity);
            return new ResponseExpenseJson();
        }
        private void Validate(RequestExpenseJson request)
        {
            var validator = new RegisterExpenseValidator();
            var result = validator.Validate(request);
            if (!result.IsValid) 
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
