using BancoMaster.Rotas.Application.Rotas.CalcularRotas.Interface;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.DataTransfer.Rotas.Response;
using BancoMaster.Rotas.Domain.Entidade.Arestas;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;

namespace BancoMaster.Rotas.Application.Rotas.CalcularRotas
{
    public class CalcularMelhorRotaServico(IRotaRepository _rotaRepository) : ICalcularMelhorRotaServico
    {

        public async Task<CalcularMelhorRotaResponse> ExecuteAsync(CalcularMelhorRotaRequest request)
        {
            IEnumerable<Rota> rotas = await _rotaRepository.GetAllAsync();

            if (rotas.Where(p => p.Destino == request.Destino.ToUpper()).Count() < 1)
                return new CalcularMelhorRotaResponse { MelhorRota = "Destino não existe no banco de dados!" };

            if (rotas.Where(p => p.Origem == request.Origem.ToUpper()).Count() < 1)
                return new CalcularMelhorRotaResponse { MelhorRota = "Origem não existe no banco de dados!" };

            var grafo = MontarGrafo(rotas);

            var melhorRota = CalcularMelhorRota(grafo, request.Origem.ToUpper(), request.Destino.ToUpper());
            string cidadesFormatadas = string.Join(" - ", melhorRota.Cidades);
            return new CalcularMelhorRotaResponse { MelhorRota = $"{cidadesFormatadas} ao custo de $ {melhorRota.CustoTotal}" };
        }

        #region Métodos Privados
        private RotaResponse CalcularMelhorRota(Dictionary<string, List<Aresta>> grafo, string origem, string destino)
        {
            var distancias = new Dictionary<string, decimal>();
            var predecessores = new Dictionary<string, string>();
            var cidadesNaoVisitadas = new HashSet<string>(grafo.Keys);

            foreach (var cidade in grafo.Keys)
            {
                distancias[cidade] = cidade == origem ? 0 : decimal.MaxValue;
                predecessores[cidade] = null;
            }

            while (cidadesNaoVisitadas.Any())
            {
                var cidadeAtual = cidadesNaoVisitadas.OrderBy(c => distancias[c]).First();
                cidadesNaoVisitadas.Remove(cidadeAtual);

                if (cidadeAtual == destino) break;

                foreach (var aresta in grafo[cidadeAtual])
                {
                    var novaDistancia = distancias[cidadeAtual] + aresta.Valor;
                    if (novaDistancia < distancias[aresta.Destino])
                    {
                        distancias[aresta.Destino] = novaDistancia;
                        predecessores[aresta.Destino] = cidadeAtual;
                    }
                }
            }

            var caminho = new List<string>();
            var cidadeNoCaminho = destino;

            while (cidadeNoCaminho != null)
            {
                caminho.Insert(0, cidadeNoCaminho);
                cidadeNoCaminho = predecessores[cidadeNoCaminho];
            }

            return new RotaResponse
            {
                Cidades = caminho,
                CustoTotal = distancias[destino]
            };
        }

        private Dictionary<string, List<Aresta>> MontarGrafo(IEnumerable<Rota> rotas)
        {
            var grafo = new Dictionary<string, List<Aresta>>();

            foreach (var rota in rotas)
            {
                if (!grafo.ContainsKey(rota.Origem))
                    grafo[rota.Origem] = [];

                grafo[rota.Origem].Add(new Aresta(rota.Destino, rota.Valor));

                if (!grafo.ContainsKey(rota.Destino))
                    grafo[rota.Destino] = [];
            }
            return grafo;
        }
        #endregion
    }
}
