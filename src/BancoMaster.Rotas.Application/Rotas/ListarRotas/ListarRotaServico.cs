using BancoMaster.Rotas.Application.Rotas.ListarRotas.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;

namespace BancoMaster.Rotas.Application.Rotas.ListarRotas;

public class ListarRotaServico(IRotaRepository _rotaRepository) : IListarRotaServico
{
    public async Task<ListarRotasResponse> ExecuteAsync(ListarRotasRequest request)
    {
        IEnumerable<Rota> rotas = await _rotaRepository.GetAllAsync();

        return new ListarRotasResponse
        {
            Rotas = rotas.ToList()
        };
    }
}
