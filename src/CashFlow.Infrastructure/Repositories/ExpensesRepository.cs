using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Database;

namespace CashFlow.Infrastructure.Repositories
{
    internal class ExpensesRepository : IExpensesRepository
    {
        public void Add(ExpenseEntity entity)
        {
            var dbContext = new CashFlowDbContext();
            dbContext.Expenses.Add(entity);
            dbContext.SaveChanges();
        }
    }
}
