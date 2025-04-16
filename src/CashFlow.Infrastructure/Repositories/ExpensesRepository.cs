using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Database;

namespace CashFlow.Infrastructure.Repositories
{
    internal class ExpensesRepository : IExpensesRepository
    {
        private readonly CashFlowDbContext _dbContext;
        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(ExpenseEntity entity)
        {
            _dbContext.Expenses.Add(entity);
            await _dbContext.SaveChangesAsync();
        }
    };

}
