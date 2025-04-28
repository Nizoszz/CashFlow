namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public interface IGenerateExpenseReportExcel
    {
        public Task<byte[]> Execute(DateOnly month);
    }
}
