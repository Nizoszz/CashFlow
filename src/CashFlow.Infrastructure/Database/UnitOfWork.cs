using CashFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Infrastructure.Database
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly CashFlowDbContext _dbContext;
        public UnitOfWork(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();

        }

        public void Rollback()
        {

        }
    }
}
