namespace CashFlow.Application.UseCases.Expenses.DeleteExpenseById
{
    public interface IDeleteExpenseByIdUseCase
    {
        Task Execute(long id);
    }
}
