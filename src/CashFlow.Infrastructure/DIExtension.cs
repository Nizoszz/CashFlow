using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure
{
    public static class DIExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IExpensesRepository, ExpensesRepository>();
        }
    }
}
