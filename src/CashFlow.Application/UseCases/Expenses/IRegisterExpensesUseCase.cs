using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses
{
    public interface IRegisterExpensesUseCase
    {
        Task<ResponseExpenseJson> Execute(RequestExpenseJson request);
      
    }
}
