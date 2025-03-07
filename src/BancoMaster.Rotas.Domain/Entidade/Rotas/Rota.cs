namespace BancoMaster.Rotas.Domain.Entidade.Rotas;

public class Rota
{
    public virtual int Id { get; protected set; }
    public virtual string Origem { get; protected set; }
    public virtual string Destino { get; protected set; }
    public virtual decimal Valor { get; protected set; }

    public Rota()
    {

    }

    public Rota(int id, string origem, string destino, decimal valor)
    {
        SetId(id);
        SetOrigem(origem);
        SetDestino(destino);
        SetValor(valor);
    }

    public Rota(string origem, string destino, decimal valor)
    {
        SetOrigem(origem);
        SetDestino(destino);
        SetValor(valor);
    }

    public void SetId(int id) => Id = id;
    public void SetOrigem(string origem) => Origem = origem;
    public void SetDestino(string destino) => Destino = destino;
    public void SetValor(decimal valor) => Valor = valor;
}
