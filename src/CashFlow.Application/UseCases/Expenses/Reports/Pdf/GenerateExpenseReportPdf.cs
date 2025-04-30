using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf
{
    public class GenerateExpenseReportPdf : IGenerateExpenseReportPdf
    {
        private const string CURRENCY_SYMBOL = "R$";
        private readonly IExpensesRepository _expensesRepository;

        public GenerateExpenseReportPdf(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
            GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
        }

        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _expensesRepository.FilterByMonth(month);
            if (expenses.Count == 0)
            {
                return [];
            }
            return [];
        }
    }
}
