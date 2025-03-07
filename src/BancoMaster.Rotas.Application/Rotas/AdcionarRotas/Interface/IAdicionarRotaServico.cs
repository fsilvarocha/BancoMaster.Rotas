using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;

namespace BancoMaster.Rotas.Application.Rotas.AdcionarRotas.Interface;

public interface IAdicionarRotaServico
{
    Task<AdicionarRotaResponse> ExecuteAsync(AdicionarRotaRequest request);
}
