namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf
{
    public interface IGenerateExpenseReportPdf
    {
        public Task<byte[]> Execute(DateOnly month);
    }
}
