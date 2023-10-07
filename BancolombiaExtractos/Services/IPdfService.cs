using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Services;

/// <summary>
///     Interfaz para el servicio que se encarga de generar pdf
/// </summary>
public interface IPdfService
{
    /// <summary>
    ///     Genera un <see cref="Stream" /> con el extracto pdf de la cuenta <paramref name="accountId" />
    /// </summary>
    /// <param name="accountId"></param>
    public Task<Result<Stream>> CreateExtractoPdfStream(int accountId);
}