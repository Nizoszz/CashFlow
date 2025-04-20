using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.UpdateExpense
{
    public interface IUpdateExpenseUseCase
    {
        Task Execute(long id, RequestExpenseJson request);
    }
}
