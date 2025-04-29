using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteById(long id)
        {
            var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            if (result is null)
            {
                return false;
            }
            _dbContext.Expenses.Remove(result);
            return true;
        }

        public async Task<List<ExpenseEntity>> FilterByMonth(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
            var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59).Date;
            return await _dbContext
                .Expenses
                .AsNoTracking()
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .OrderBy(e => e.Date)
                .ThenBy(e => e.Title)
                .ToListAsync();
        }

        public async Task<List<ExpenseEntity>> GetAll()
        {
            return await _dbContext.Expenses.AsNoTracking().ToListAsync();
        }

        public async Task<ExpenseEntity?> GetById(long id)
        {
            return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(ExpenseEntity entity)
        {
            _dbContext.Expenses.Update(entity);
        }
    };

}
