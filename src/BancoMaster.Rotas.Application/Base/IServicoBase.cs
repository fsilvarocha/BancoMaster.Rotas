namespace BancoMaster.Rotas.Application.Base;

public interface IServicoBase<TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request);
}
