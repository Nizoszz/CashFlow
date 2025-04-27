using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public class GenerateExpenseReportExcel : IGenerateExpenseReportExcel
    {
        public async Task<byte[]> Execute(DateOnly month)
        {
            var workbook = new XLWorkbook();
            workbook.Author = "Nizos";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "New Times Roman";
            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        }
    }
}
