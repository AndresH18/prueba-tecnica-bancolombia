﻿using BancolombiaExtractos.Data;
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