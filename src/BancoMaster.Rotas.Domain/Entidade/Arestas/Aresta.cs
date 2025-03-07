namespace BancoMaster.Rotas.Domain.Entidade.Arestas;

public class Aresta
{
    public virtual string Destino { get; protected set; }
    public virtual decimal Valor { get; protected set; }

    public Aresta(string destino, decimal valor)
    {
        SetDestino(destino);
        SetValor(valor);
    }

    public void SetDestino(string destino) => Destino = destino;
    public void SetValor(decimal valor) => Valor = valor;
}
