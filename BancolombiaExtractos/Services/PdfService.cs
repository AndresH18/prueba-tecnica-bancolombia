using BancolombiaExtractos.Data;
using BancolombiaExtractos.Data.Repository;
using BancolombiaExtractos.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Services;

public class PdfService : IPdfService
{
    private readonly SemaphoreSlim _semaphore = new(1);
    private readonly IRepository _repository;
    private readonly PdfGenerator _pdfGenerator;

    public PdfService(IRepository repository, PdfGenerator pdfGenerator)
    {
        _repository = repository;
        _pdfGenerator = pdfGenerator;
    }

    public async Task<Result<Stream>> CreateExtractoPdfStream(int accountId)
    {
        await _semaphore.WaitAsync();
        try
        {
            var movimientos = await _repository.GetMovimientosExtracto(accountId);
            if (!movimientos)
                return new Result<Stream>();
            
            
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