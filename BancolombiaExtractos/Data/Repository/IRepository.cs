using BancolombiaExtractos.Data.Models;
using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Data.Repository;

/// <summary>
///     Interfaz para encapsular la fuente de información, utilizando el patron de repositorio
/// </summary>
public interface IRepository
{
    /// <summary>
    ///     Revisar que las credenciales <paramref name="email" /> y <paramref name="accountId" /> concuerden con la
    ///     informacion de la base de datos
    /// </summary>
    /// <param name="email">El email del titular</param>
    /// <param name="accountId">El numero de la cuenta del titular y de la que se quiere generar el extracto</param>
    /// <returns>
    ///     Una <see cref="Task" /> que representa la operacion, de <see cref="Result{TValue}" /> donde TValue es
    ///     <see cref="bool" />. <b>true</b> si las credenciales son correctas, <b>false</b> si no
    /// </returns>
    public Task<Result<bool>> IsCredentialsValid(string email, int accountId);

    /// <summary>
    ///     Returns a <see cref="Result{TValue}" /> where TValue is a <see cref="Cuenta" /> with its
    ///     <see cref="Cuenta.Usuario" /> and its <see cref="Cuenta.Movimientos" /> for the last month
    /// </summary>
    /// <param name="accountId">The id of the <see cref="Cuenta" /></param>
    /// <returns>
    ///     An <see cref="Cuenta" /> with its <see cref="Usuario" /> information and the <see cref="Cuenta.Movimientos" />
    ///     of the last month
    /// </returns>
    /// <seealso cref="Result{TValue}" />
    public Task<Result<Cuenta>> GetCuentaExtracto(int accountId);

    /// <summary>
    ///     Obtiene una <see cref="Cuenta" />, con su usuario, aleatoria que tenga <see cref="Movimiento" /> del mes anterior
    /// </summary>
    /// <returns>Una <see cref="Task{TResult}" /> donde TResult es <see cref="Cuenta" /> con movimientos del ultimo mes</returns>
    /// <remarks>
    ///     La intencion es que sirva para la prueba para sugerir credenciales que se puedan usar
    /// </remarks>
    public Task<Cuenta> GetRandom();
}