using CashFlow.Application.UseCases.Expenses;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application

{
    public static class DIExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
        }
    }
}
