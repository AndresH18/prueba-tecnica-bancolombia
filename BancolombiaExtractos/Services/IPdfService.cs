using BancolombiaExtractos.Helpers;

namespace BancolombiaExtractos.Services;

public interface IPdfService
{
    public Task<Result<Stream>> CreateExtractoPdfStream(int accountId);
}