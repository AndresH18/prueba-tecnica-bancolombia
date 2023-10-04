using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Data.Repository;

public interface IRepository
{
    public Task<Result<bool>> IsCredentialsValid(string email, int accountId);
    public Task<Result<IEnumerable<Movimiento>>> GetMovimientosExtracto(int accountId);
}