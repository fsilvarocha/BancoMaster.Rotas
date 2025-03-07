using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;

namespace BancoMaster.Rotas.Application.Rotas.CalcularRotas.Interface
{
    public interface ICalcularMelhorRotaServico
    {
        Task<CalcularMelhorRotaResponse> ExecuteAsync(CalcularMelhorRotaRequest request);
    }
}
