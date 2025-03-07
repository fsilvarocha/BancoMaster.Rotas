using BancoMaster.Rotas.Domain.Entidade.Rotas;

namespace BancoMaster.Rotas.DataTransfer.Rotas.Response;

public class ListarRotasResponse
{
    public List<Rota> Rotas { get; set; } = [];
}
