using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses
{
    public interface IRegisterExpensesUseCase
    {
        ResponseExpenseJson Execute(RequestExpenseJson request);
      
    }
}
