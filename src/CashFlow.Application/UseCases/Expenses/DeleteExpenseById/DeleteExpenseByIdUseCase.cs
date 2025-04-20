
using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses.DeleteExpenseById
{
    class DeleteExpenseByIdUseCase : IDeleteExpenseByIdUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteExpenseByIdUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork)
        {
            _expensesRepository = expensesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _expensesRepository.DeleteById(id) ? true : throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE);
                await _unitOfWork.CommitAsync();
            }
            catch(System.Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
