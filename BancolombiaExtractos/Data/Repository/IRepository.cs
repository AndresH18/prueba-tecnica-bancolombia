using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Data.Repository;

public interface IRepository
{
    public Task<Result<bool>> IsCredentialsValid(string email, int accountId);

    /// <summary>
    /// Returns a <see cref="Result{TValue}"/> where TValue is a <see cref="Cuenta"/> with its <see cref="Cuenta.Usuario"/> and its <see cref="Cuenta.Movimientos"/> for the last month
    /// </summary>
    /// <param name="accountId">The id of the <see cref="Cuenta"/></param>
    /// <returns>An <see cref="Cuenta"/> with its <see cref="Usuario"/> information and the <see cref="Cuenta.Movimientos"/> of the last month</returns>
    /// <seealso cref="Result{TValue}"/>
    public Task<Result<Cuenta>> GetCuentaExtracto(int accountId);

    public Task<Cuenta> GetRandom();
}