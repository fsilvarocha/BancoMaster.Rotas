namespace BancoMaster.Rotas.DataTransfer.Rotas.Request;

public class CalcularMelhorRotaRequest
{
    public required string Destino { get; set; }
    public required string Origem { get; set; }
}
