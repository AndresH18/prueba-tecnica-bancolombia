using System.Globalization;
using BancolombiaExtractos.Data.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;


namespace BancolombiaExtractos.Services;

public class PdfGenerator
{
    public byte[] GeneratePdf(Cuenta cuenta)
    {
        var stream = new MemoryStream();

        var pdfDocument = new PdfDocument(new PdfWriter(stream));

        var document = new Document(pdfDocument);

        // document.Add(new Image)
        document.Add(new Paragraph(
            $"""
             Extracto Bancario

             Información del Titular :
                Nombre: {cuenta.Usuario.Titular}
                Correo: {cuenta.Usuario.Email}
                
             Fecha Extracto: {DateTime.Now.AddMonths(-1).ToString(CultureInfo.CurrentCulture)} - {DateTime.Now.ToString(CultureInfo.CurrentCulture)}

             Cuenta: #{cuenta.NumeroCuenta}
             Saldo a la fecha: {cuenta.Saldo}

             Ingresos Mes: {cuenta.Movimientos.Where(m => m.Valor > 0).Sum(m => m.Valor)}
             Egresos: {cuenta.Movimientos.Where(m => m.Valor < 0).Sum(m => m.Valor)}
             """
        ));

        var table = new Table(UnitValue.CreatePercentArray(2));
        table.AddHeaderCell("Fecha").AddHeaderCell("Valor");

        foreach (var movimiento in cuenta.Movimientos.OrderByDescending(m => m.Fecha))
        {
            table.AddCell(movimiento.Fecha.ToShortDateString());
            table.AddCell(movimiento.Valor.ToString(CultureInfo.CurrentCulture));
        }

        document.Add(table);

        document.Close();

        return stream.ToArray();
    }
}