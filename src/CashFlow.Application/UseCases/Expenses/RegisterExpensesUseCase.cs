using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses
{
    public class RegisterExpensesUseCase : IRegisterExpensesUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterExpensesUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork)        {
            _expensesRepository = expensesRepository;
            _unitOfWork = unitOfWork;
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
            _unitOfWork.Commit();
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
