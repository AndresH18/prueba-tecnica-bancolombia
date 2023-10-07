// See https://aka.ms/new-console-template for more information

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

var filePath = Path.Combine(Environment.CurrentDirectory, "extracto.pdf");

Console.WriteLine(filePath);
// var ms = new MemoryStream();
var pdfDocument = new PdfDocument(new PdfWriter($"{filePath}"));

var document = new Document(pdfDocument);

document.Add(new Paragraph("Extracto Bancario"));
document.Add(new Paragraph("Cliente --cliente"));
document.Add(new Paragraph("Fecha   --fecha"));
document.Add(new Paragraph("Numero Cuenta --numero cuenta"));

document.Close();