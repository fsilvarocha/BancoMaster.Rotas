using BancoMaster.Rotas.Application.Rotas.AdcionarRotas.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;

namespace BancoMaster.Rotas.Application.Rotas.AdcionarRotas
{
    public class AdicionarRotaServico(IRotaRepository _rotaRepository) : IAdicionarRotaServico
    {
        public async Task<AdicionarRotaResponse> ExecuteAsync(AdicionarRotaRequest request)
        {
            if (request == null)
                return new AdicionarRotaResponse
                {
                    Sucesso = false,
                    Mensagem = "Requisição inválida"
                };

            if (string.IsNullOrEmpty(request.Origem) || string.IsNullOrEmpty(request.Destino) || request.Valor <= 0)
                return new AdicionarRotaResponse
                {
                    Sucesso = false,
                    Mensagem = "Dados da rota inválidos"
                };

            Rota rota = new();
            rota.SetOrigem(request.Origem);
            rota.SetDestino(request.Destino);
            rota.SetValor(request.Valor);

            await _rotaRepository.AddAsync(rota);

            return new AdicionarRotaResponse
            {
                Sucesso = true,
                Mensagem = "Rota adicionada com sucesso"
            };
        }
    }
}
