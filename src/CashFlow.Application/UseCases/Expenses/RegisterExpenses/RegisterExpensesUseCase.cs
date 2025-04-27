using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Validator;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.RegisterExpenses
{
    public class RegisterExpensesUseCase : IRegisterExpensesUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterExpensesUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper)        
        {
            _expensesRepository = expensesRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseExpenseJson> Execute(RequestExpenseJson request)
        {
            Validate(request);
            var entity = _mapper.Map<ExpenseEntity>(request);
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _expensesRepository.Add(entity);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ResponseExpenseJson>(entity);
            }
            catch (System.Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        private static void Validate(RequestExpenseJson request)
        {
            var validator = new RequestExpenseValidator();
            var result = validator.Validate(request);
            if (!result.IsValid) 
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
