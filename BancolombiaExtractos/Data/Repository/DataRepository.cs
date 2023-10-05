using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data.Repository;

public class DataRepository : IRepository
{
    private readonly SemaphoreSlim _semaphore = new(1);
    private readonly PruebaBancolombiaContext _db;

    public DataRepository(PruebaBancolombiaContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> IsCredentialsValid(string email, int accountId)
    {
        await _semaphore.WaitAsync();
        try
        {
            var account = await _db.Cuentas.Include(c => c.Usuario)
                .Where(c => c.NumeroCuenta == accountId && c.Usuario.Email.Equals(email)).FirstOrDefaultAsync();

            return account is not null;
        }
        catch
        {
            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Result<Cuenta>> GetCuentaExtracto(int accountId)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await _db.Cuentas
                .Include(c => c.Usuario)
                .Include(c => c.Movimientos
                    .Where(m => m.Fecha.Month == DateTime.Now.AddMonths(-1).Month))
                .FirstAsync(c => c.NumeroCuenta == accountId);
        }
        catch
        {
            return new Result<Cuenta>();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<Cuenta> GetRandom()
    {
        var randomNumber = Random.Shared.Next(1000) + 1;

        return await _db.Cuentas
            .Where(c => c.Movimientos.Any())
            .Include(c => c.Usuario)
            .FirstAsync(c => c.NumeroCuenta == randomNumber);
    }
}