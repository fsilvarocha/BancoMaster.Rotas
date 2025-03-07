using BancoMaster.Rotas.Application.Rotas.EditarRotas.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;

namespace BancoMaster.Rotas.Application.Rotas.EditarRotas;

public class EditarRotaServico(IRotaRepository _rotaRepository) : IEditarRotaServico
{
    public async Task<EditarRotaResponse> ExecuteAsync(EditarRotaRequest request)
    {
        Rota rota = await _rotaRepository.GetByIdAsync(request.RotaId);

        if (rota == null)
        {
            throw new Exception("Rota não encontrada.");
        }

        if (string.IsNullOrEmpty(request.Origem) || string.IsNullOrEmpty(request.Destino) || request.Valor <= 0)
            return new EditarRotaResponse
            {
                Sucesso = false,
                Mensagem = "Dados da rota inválidos"
            };

        rota.SetOrigem(request.Origem.ToUpper());
        rota.SetDestino(request.Destino.ToUpper());
        rota.SetValor(request.Valor);

        await _rotaRepository.UpdateAsync(rota);

        return new EditarRotaResponse
        {
            Sucesso = true,
            Mensagem = "Rota atualizada com sucesso."
        };
    }
}
