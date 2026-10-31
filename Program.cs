using Reporter.Engine.Data;
using Reporter.Engine.Format;
using Reporter.Engine.Formula;
using Reporter.Engine.Layout;
using Reporter.Engine.Render;
using Reporter.Model;
using Reporter.Rpt;

string input = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "order-summary.rpt");
ReportModel model = ReportModelBuilder.FromRpt(input);

IReadOnlyDictionary<string, FormulaValue> parameters = ParameterBinding.Resolve(
    model, new Dictionary<string, string>(), useSavedValues: true);
model = ReportDataBinder.Bind(model, new SavedDataRowSource(), parameters, input);

PaginatedDocument document = Paginator.Paginate(model, new PdfTextMeasurer(), Locale.EnGb, parameters);
PdfRenderer.RenderToFile(document, model, "out.pdf");
Console.WriteLine($"Created {Path.GetFullPath("out.pdf")} ({document.Pages.Count} pages).");
