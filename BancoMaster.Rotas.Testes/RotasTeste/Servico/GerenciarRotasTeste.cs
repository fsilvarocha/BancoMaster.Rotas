using BancoMaster.Rotas.Application.Rotas.AdcionarRotas;
using BancoMaster.Rotas.Application.Rotas.EditarRotas;
using BancoMaster.Rotas.Application.Rotas.ExcluirRota;
using BancoMaster.Rotas.Application.Rotas.ListarRotas;
using BancoMaster.Rotas.DataTransfer.Rotas.Request;
using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;
using Moq;

namespace BancoMaster.Rotas.Testes.RotasTeste.Servico;

public class GerenciarRotasTeste
{

    private readonly Mock<IRotaRepository> _mockRepository;
    private readonly AdicionarRotaServico _adicionarUseCase;
    private readonly EditarRotaServico _editarUseCase;
    private readonly ExcluirRotaServico _excluirUseCase;
    private readonly ListarRotaServico _listarUseCase;

    public GerenciarRotasTeste()
    {
        _mockRepository = new Mock<IRotaRepository>();
        _adicionarUseCase = new AdicionarRotaServico(_mockRepository.Object);
        _editarUseCase = new EditarRotaServico(_mockRepository.Object);
        _excluirUseCase = new ExcluirRotaServico(_mockRepository.Object);
        _listarUseCase = new ListarRotaServico(_mockRepository.Object);
    }

    [Fact]
    public async Task DeveAdicionarNovaRota()
    {
        // Arrange
        var request = new AdicionarRotaRequest { Origem = "GRU", Destino = "JFK", Valor = 100 };

        Rota rota = new();
        rota.SetOrigem("GRU");
        rota.SetDestino("JFK");
        rota.SetValor(100);

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Rota>())).Returns(Task.FromResult(rota));

        // Act
        var resultado = await _adicionarUseCase.ExecuteAsync(request);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.Equal("Rota adicionada com sucesso", resultado.Mensagem);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Rota>()), Times.Once);

    }

    [Fact]
    public async Task DeveEditarRota()
    {
        // Arrange
        var request = new EditarRotaRequest { RotaId = 1, Origem = "GRU", Destino = "LAX", Valor = 150 };
        Rota rotaEditada = new(1, "GRU", "LAX", 150);

        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(rotaEditada);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Rota>())).Returns(Task.CompletedTask);

        // Act
        var resultado = await _editarUseCase.ExecuteAsync(request);

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.Equal("Rota atualizada com sucesso.", resultado.Mensagem);
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Rota>()), Times.Once);
    }

    [Fact]
    public async Task DeveExcluirRota()
    {
        // Arrange
        Rota rota = new();
        rota.SetId(1);
        rota.SetDestino("GRU");
        rota.SetOrigem("BRC");

        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(rota);
        _mockRepository.Setup(r => r.RemoveAsync(rota)).Returns(Task.CompletedTask);

        // Act
        var resultado = await _excluirUseCase.ExecuteAsync(new ExcluirRotaRequest { RotaId = rota.Id });

        // Assert
        Assert.True(resultado.Sucesso);
        Assert.Equal("Rota excluída com sucesso.", resultado.Mensagem);
        _mockRepository.Verify(r => r.RemoveAsync(rota), Times.Once);
    }

    [Fact]
    public async Task DeveListarTodasAsRotas()
    {
        // Arrange
        var rotas = new List<Rota>
        {
            new ("GRU","BRC",10),
            new ("BRC","SCL",5)
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(rotas);

        // Act
        var resultado = await _listarUseCase.ExecuteAsync(new ListarRotasRequest());

        // Assert
        Assert.Equal(rotas.Count, resultado.Rotas.Count);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
