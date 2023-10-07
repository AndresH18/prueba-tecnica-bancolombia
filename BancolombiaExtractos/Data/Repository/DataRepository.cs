using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data.Repository;

/// <summary>
///     Implementacion de <see cref="IRepository" />, utilizando de <i>thread-safety</i>
/// </summary>
public class DataRepository : IRepository
{
    private readonly PruebaBancolombiaContext _db;
    private readonly SemaphoreSlim _semaphore = new(1);

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
            var c = await _db.Cuentas
                .Include(c => c.Usuario)
                .Include(c => c.Movimientos
                    .Where(m => m.Fecha.Month == DateTime.Now.AddMonths(-1).Month))
                .FirstAsync(c => c.NumeroCuenta == accountId);

            return c;
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

    /// <inheritdoc />
    /// <remarks>
    ///     Busca cuantos <see cref="Cuenta" />s tienen movimientos del ultimo mes.<br />
    ///     Obtiene la primera cuenta que tuvo movimientos el ultimo mes despues de saltarse un numero entero aleatorio de
    ///     registros
    /// </remarks>
    public async Task<Cuenta> GetRandom()
    {
        var accountsWithMovements = await _db.Database.SqlQuery<int>(
            $"""
             SELECT COUNT(DISTINCT c.numero_cuenta) AS Value
             FROM cuentas c
                INNER JOIN dbo.movimientos m ON c.numero_cuenta = m.numero_cuenta
             WHERE MONTH(m.fecha) = MONTH(GETDATE()) - 1
             """).FirstAsync();

        var account = await _db.Cuentas
            .Where(c => c.Movimientos.Any(m => m.Fecha.Month == DateTime.Now.AddMonths(-1).Month))
            .Include(c => c.Usuario)
            .Skip(Random.Shared.Next(accountsWithMovements))
            .FirstAsync();

        return account;
    }
}