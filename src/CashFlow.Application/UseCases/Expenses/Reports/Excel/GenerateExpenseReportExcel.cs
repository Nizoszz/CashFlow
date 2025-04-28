using ClosedXML.Excel;
using CashFlow.Domain.Reports;
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
            InsertHeader(worksheet);
            var file = new MemoryStream();
            workbook.SaveAs(file);
            return file.ToArray();
        }
        private void InsertHeader (IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;
            worksheet.Cells("A1:E1").Style.Font.Bold = true;
            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#0C65EE");
            worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        }
    }
}
