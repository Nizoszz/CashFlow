using CashFlow.Application.Mapper;
using CashFlow.Application.UseCases.Expenses.DeleteExpenseById;
using CashFlow.Application.UseCases.Expenses.GetAllExpenses;
using CashFlow.Application.UseCases.Expenses.GetExpenseById;
using CashFlow.Application.UseCases.Expenses.RegisterExpenses;
using CashFlow.Application.UseCases.Expenses.Report.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Application.UseCases.Expenses.UpdateExpense;
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
            services.AddScoped<IDeleteExpenseByIdUseCase, DeleteExpenseByIdUseCase>();
            services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();
            services.AddScoped<IGenerateExpenseReportExcel, GenerateExpenseReportExcel>();
            services.AddScoped<IGenerateExpenseReportPdf, GenerateExpenseReportPdf>();
        }
    }
}
