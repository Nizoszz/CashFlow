
using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Validator;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.UpdateExpense
{
    class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpensesRepository _expensesRepository;

        public UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpensesRepository expensesRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _expensesRepository = expensesRepository;
        }

        public async Task Execute(long id, RequestExpenseJson request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _expensesRepository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
                Validate(request);
                _mapper.Map(request, result);
                _expensesRepository.Update(result);
                await _unitOfWork.CommitAsync();
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
