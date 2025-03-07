using BancoMaster.Rotas.Application.Rotas.CalcularRotas;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;
using Moq;

namespace BancoMaster.Rotas.Testes.RotasTeste.Servico
{
    public class CalcularMelhorRotaTeste
    {
        private readonly Mock<IRotaRepository> _mockRepository;
        private readonly CalcularMelhorRotaServico _useCase;

        public CalcularMelhorRotaTeste()
        {
            _mockRepository = new Mock<IRotaRepository>();
            _useCase = new CalcularMelhorRotaServico(_mockRepository.Object);
        }

        [Fact]
        public async void DeveCalcularMelhorRota()
        {
            // Arrange
            var rotas = new List<Rota>{
            new(1,"GRU","BRC",10 ),
            new(2,"BRC","SCL",5 ),
            new(3,"GRU","CDG",75 ),
            new(4,"GRU","SCL",20 ),
            new(5,"GRU","ORL",56 ),
            new(6,"ORL","CDG",5 ),
            new(7,"SCL","ORL",20 )};

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(rotas);
            // Act
            var resultado = await _useCase.ExecuteAsync(new CalcularMelhorRotaRequest() { Origem = "GRU", Destino = "CDG" });
            // Assert
            Assert.Equal("GRU - BRC - SCL - ORL - CDG ao custo de $ 40", resultado.MelhorRota);
        }
    }
}
