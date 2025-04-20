using CashFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CashFlow.Infrastructure.Database
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly CashFlowDbContext _dbContext;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await _dbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
