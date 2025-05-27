using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

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
            var document = CreateDocument(month);
            var page = CreatePage(document);
            var totalExpenses = expenses.Sum(expense => expense.Amount);
            CreateHeaderWithProfilePhotoAndName(page);
            CreateTotalSpentSection(page, month, totalExpenses);
            return RenderDocument(document);
        }
        private Document CreateDocument(DateOnly month) 
        { 
            var document = new Document();
            document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y}";
            document.Info.Author = "N1z0";
            var style = document.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;
            return document;
        }
        private Section CreatePage(Document document) 
        { 
            var section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.PageSetup.TopMargin = 80;
            section.PageSetup.BottomMargin = 80;
            return section;
        }

        private void CreateHeaderWithProfilePhotoAndName(Section page) 
        {
            var table = page.AddTable();
            table.AddColumn();
            table.AddColumn("300");
            var row = table.AddRow();
            var assembly = Assembly.GetExecutingAssembly();
            var directoryName = Path.GetDirectoryName(assembly.Location);
            var image = row.Cells[0].AddImage(Path.Combine(directoryName!, "Logo", "51V+1wd2dFL.jpg"));
            image.Width = Unit.FromCentimeter(2.3);
            image.Height = Unit.FromCentimeter(2.3);
            row.Cells[1].AddParagraph(ResourceReportGenerationMessages.GREETINGS);
            row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
            row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        }
        private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpenses)
        {
            var paragraph = page.AddParagraph();
            paragraph.Format.SpaceBefore = "40";
            paragraph.Format.SpaceAfter = "40";
            var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));
            paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 16 });
            paragraph.AddLineBreak();
            paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalExpenses}", new Font { Name = FontHelper.WORK_SANS_BLACK, Size = 40 });
        }
        private byte[] RenderDocument(Document document) 
        {
            var renderer = new PdfDocumentRenderer
            { 
                Document = document,
            };
            renderer.RenderDocument();
            using var file = new MemoryStream();
            renderer.PdfDocument.Save(file);
            return file.ToArray();
        }
    }
}
