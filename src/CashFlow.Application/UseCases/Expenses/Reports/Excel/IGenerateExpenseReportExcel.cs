namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public interface IGenerateExpenseReportExcel
    {
        public async Task<byte[]> Execute(DateOnly month) 
        { 
        }
    }
}
