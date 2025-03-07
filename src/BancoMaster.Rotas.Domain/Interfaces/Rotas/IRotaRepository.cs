using BancoMaster.Rotas.Domain.Entidade.Rotas;

namespace BancoMaster.Rotas.Domain.Interfaces.Rotas;

public interface IRotaRepository
{
    Task AddAsync(Rota rota);
    Task<IEnumerable<Rota>> GetAllAsync();
    Task<Rota?> GetByIdAsync(int id);
    Task RemoveAsync(Rota rota);
    Task UpdateAsync(Rota rota);
}
