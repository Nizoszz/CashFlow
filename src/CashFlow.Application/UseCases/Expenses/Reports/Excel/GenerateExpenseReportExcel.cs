using ClosedXML.Excel;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Enums;
namespace CashFlow.Application.UseCases.Expenses.Report.Excel
{
    public class GenerateExpenseReportExcel : IGenerateExpenseReportExcel
    {
        private const string CURRENCY_SYMBOL = "R$";
        private readonly IExpensesRepository _expensesRepository;
        public GenerateExpenseReportExcel(IExpensesRepository expensesRepository) { 
            _expensesRepository = expensesRepository;            
        }
        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _expensesRepository.FilterByMonth(month);
            if (expenses.Count == 0) {
                return [];
            }
            using var workbook = new XLWorkbook();
            workbook.Author = "Nizos";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "New Times Roman";
            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
            InsertHeader(worksheet);
            var count = 2;
            foreach (var expense in expenses) {
                worksheet.Cell($"A{count}").Value = expense.Title;
                worksheet.Cell($"B{count}").Value = expense.Date;
                worksheet.Cell($"C{count}").Value = ConvertPaymentType(expense.PaymentType);
                worksheet.Cell($"D{count}").Value = expense.Amount;
                worksheet.Cell($"D{count}").Style.NumberFormat.Format = $"- {CURRENCY_SYMBOL} #,##0.00";
                worksheet.Cell($"E{count}").Value = expense.Description;
                count++;
            }
            worksheet.Columns().AdjustToContents();
            var file = new MemoryStream();
            workbook.SaveAs(file);
            return file.ToArray();
        }
        private string ConvertPaymentType(PaymentType payment) 
        {
            return payment switch
            {
                PaymentType.Cash => ResourcePaymentType.CASH,
                PaymentType.CreditCard => ResourcePaymentType.CREDIT_CARD,
                PaymentType.BankTransfer => ResourcePaymentType.BANK_TRANSFER,
                PaymentType.DebitCard => ResourcePaymentType.DEBIT_CARD,
                _ => ResourcePaymentType.PAYMENT_NOT_FOUND
            };
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
