using BancoMaster.Rotas.Application.Base;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;

namespace BancoMaster.Rotas.Application.Rotas.ListarRotas.Interface;

public interface IListarRotaServico : IServicoBase<ListarRotasRequest, ListarRotasResponse>
{
}
