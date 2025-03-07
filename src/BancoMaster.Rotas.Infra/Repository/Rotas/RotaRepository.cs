using BancoMaster.Rotas.Domain.Entidade.Rotas;
using BancoMaster.Rotas.Domain.Interfaces.Rotas;
using BancoMaster.Rotas.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace BancoMaster.Rotas.Infra.Repository.Rotas;

public class RotaRepository(BancoMasterContext context) : IRotaRepository
{
    private readonly BancoMasterContext _context = context;

    public async Task AddAsync(Rota rota)
    {
        _context.Rotas.Add(rota);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Rota>> GetAllAsync() =>
        await _context.Rotas.ToListAsync();

    public async Task<Rota?> GetByIdAsync(int id) =>
        await _context.Rotas.FirstOrDefaultAsync(r => r.Id == id);

    public async Task RemoveAsync(Rota rota)
    {
        if (rota != null)
        {
            _context.Rotas.Remove(rota);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Rota rota)
    {
        _context.Rotas.Update(rota);
        await _context.SaveChangesAsync();
    }
}

