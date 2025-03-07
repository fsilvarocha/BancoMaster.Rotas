using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;

namespace BancoMaster.Rotas.Application.Rotas.EditarRotas.Interface;

public interface IEditarRotaServico
{
    Task<EditarRotaResponse> ExecuteAsync(EditarRotaRequest request);
}
