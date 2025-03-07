namespace BancoMaster.Rotas.DataTransfer.Rotas.Request;

public class AdicionarRotaRequest
{
    public required string Origem { get; set; }
    public required string Destino { get; set; }
    public decimal Valor { get; set; }
}
