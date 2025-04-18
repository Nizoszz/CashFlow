using CashFlow.Application.Mapper;
using CashFlow.Application.UseCases.Expenses;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application

{
    public static class DIExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddExpenseMapper(services);
            AddUseCases(services);
        }
        private static void AddExpenseMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ExpensesMapper));    
        }
        private static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IRegisterExpensesUseCase, RegisterExpensesUseCase>();
            services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
            services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        }
    }
}
