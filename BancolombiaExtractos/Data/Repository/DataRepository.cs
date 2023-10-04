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

    public async Task<Result<IEnumerable<Movimiento>>> GetMovimientosExtracto(int accountId)
    {
        await _semaphore.WaitAsync();
        try
        {
            return await _db.Movimientos.Where(m => m.NumeroCuenta == accountId && m.Fecha.Month == DateTime.Now.Month)
                .ToListAsync();
        }
        catch
        {
            return new Result<IEnumerable<Movimiento>>(Enumerable.Empty<Movimiento>());
        }
        finally
        {
            _semaphore.Release();
        }
    }
}