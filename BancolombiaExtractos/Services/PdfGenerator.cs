using BancolombiaExtractos.Data.Models;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace BancolombiaExtractos.Services;

/// <summary>
///     Generador del Documento PDF
/// </summary>
public class PdfGenerator
{
    /// <summary>
    ///     Genera un pdf con el extracto de la cuenta del usuario.
    /// </summary>
    /// <param name="cuenta">La cuenta del <see cref="Usuario" /> con sus <see cref="Movimiento" />s de la cuenta</param>
    /// <returns>Un arreglo de bytes de <see cref="Stream" /> del pedf</returns>
    public byte[] GeneratePdf(Cuenta cuenta)
    {
        var stream = new MemoryStream();

        var pdfDocument = new PdfDocument(new PdfWriter(stream));

        var document = new Document(pdfDocument);

        var imageDataLogo = ImageDataFactory.Create(@"wwwroot/img/logo_1.png");
        var imageDataBar = ImageDataFactory.Create(@"wwwroot/img/horizontal_bar_color.png");

        if (imageDataLogo is not null && imageDataBar is not null)
        {
            var image = new Image(imageDataLogo);
            document.Add(image);
            image = new Image(imageDataBar);
            document.Add(image);
        }

        document.Add(new Paragraph(
            $"""
             Extracto Bancario

             Información del Titular :
                Nombre: {cuenta.Usuario.Titular}
                Correo: {cuenta.Usuario.Email}
                
             Fecha Extracto: {DateTime.Now.AddMonths(-1).ToShortDateString()} - {DateTime.Now.ToShortDateString()}

             Cuenta: #{cuenta.NumeroCuenta}
             Saldo a la fecha: {cuenta.Saldo:C}

             Ingresos Mes: {cuenta.Movimientos.Where(m => m.Valor > 0).Sum(m => m.Valor):C}
             Egresos: {cuenta.Movimientos.Where(m => m.Valor < 0).Sum(m => m.Valor):C}
             """
        ));

        var table = new Table(UnitValue.CreatePercentArray(2), true);
        table.AddHeaderCell("Fecha").AddHeaderCell("Valor");

        foreach (var movimiento in cuenta.Movimientos.OrderByDescending(m => m.Fecha))
        {
            table.AddCell(movimiento.Fecha.ToShortDateString());
            table.AddCell($"{movimiento.Valor:C}");
        }

        document.Add(table);

        document.Close();

        return stream.ToArray();
    }
}