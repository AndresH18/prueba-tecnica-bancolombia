using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Data.Repository;
using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Services;

/// <summary>
///     Implementación de <see cref="IPdfService" /> para generar un <see cref="Stream" /> con el extracto en pdf
/// </summary>
public class PdfService : IPdfService
{
    private readonly PdfGenerator _pdfGenerator;
    private readonly IRepository _repository;
    private readonly SemaphoreSlim _semaphore = new(1);

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="repository">Repositorio de la información</param>
    /// <param name="pdfGenerator">Generador de pdf</param>
    public PdfService(IRepository repository, PdfGenerator pdfGenerator)
    {
        _repository = repository;
        _pdfGenerator = pdfGenerator;
    }

    /// <summary>
    ///     Crea un <see cref="Result{TValue}" /> que contiene <see cref="Stream" /> con el pdf si es exitosa la operación
    /// </summary>
    /// <param name="accountId">Id de la <see cref="Cuenta" /></param>
    /// <returns>
    ///     Una <see cref="Task{TResult}" /> de <see cref="Result{TValue}" /> que representa el proceso de generación del
    ///     pdf
    /// </returns>
    public async Task<Result<Stream>> CreateExtractoPdfStream(int accountId)
    {
        await _semaphore.WaitAsync();
        try
        {
            var cuenta = await _repository.GetCuentaExtracto(accountId);

            if (cuenta.IsError || !cuenta.Value!.Movimientos.Any())
                return new Result<Stream>();

            return new MemoryStream(_pdfGenerator.GeneratePdf(cuenta.Value!));
        }
        catch
        {
            return new Result<Stream>();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}