namespace BancoMaster.Rotas.DataTransfer.Retorno;

public class MensagemResponse
{
    public bool Sucesso { get; set; }
    public required string Mensagem { get; set; }
}
