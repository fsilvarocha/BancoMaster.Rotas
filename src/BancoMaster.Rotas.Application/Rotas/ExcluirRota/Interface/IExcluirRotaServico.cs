using BancoMaster.Rotas.Application.Base;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;

namespace BancoMaster.Rotas.Application.Rotas.ExcluirRota.Interface;

public interface IExcluirRotaServico : IServicoBase<ExcluirRotaRequest, ExcluirRotaResponse>
{
}
