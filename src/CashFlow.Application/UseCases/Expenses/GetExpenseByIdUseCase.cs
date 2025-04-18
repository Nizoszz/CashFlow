using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses
{
    class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;

        public GetExpenseByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper)
        {
            _expensesRepository = expensesRepository;
            _mapper = mapper;
        }

        public async Task<ResponseExpenseJson> Execute (long id)
        {
            var result = await _expensesRepository.GetById(id);
            return result == null
                ? throw new NotFoundException(ResourceErrorMessages.NOT_FOUND_EXPENSE)
                : _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
