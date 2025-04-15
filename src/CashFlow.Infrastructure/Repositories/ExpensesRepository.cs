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
        public void Add(ExpenseEntity entity)
        {
            _dbContext.Expenses.Add(entity);
            _dbContext.SaveChanges();
        }
    };

}
