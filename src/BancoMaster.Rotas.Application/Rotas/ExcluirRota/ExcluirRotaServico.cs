using BancoMaster.Rotas.Application.Rotas.ExcluirRota.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;

namespace BancoMaster.Rotas.Application.Rotas.ExcluirRota;

public class ExcluirRotaServico(IRotaRepository _rotaRepository) : IExcluirRotaServico
{
    public async Task<ExcluirRotaResponse> ExecuteAsync(ExcluirRotaRequest request)
    {
        Rota rota = await _rotaRepository.GetByIdAsync(request.RotaId) ?? throw new Exception("Rota não encontrada.");

        await _rotaRepository.RemoveAsync(rota);

        return new ExcluirRotaResponse
        {
            Sucesso = true,
            Mensagem = "Rota excluída com sucesso."
        };
    }
}
