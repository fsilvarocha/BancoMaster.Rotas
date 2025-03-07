using BancoMaster.Rotas.Domain.Entidade.Rotas;
using Microsoft.EntityFrameworkCore;

namespace BancoMaster.Rotas.Infra.Data;

public class BancoMasterContext(DbContextOptions<BancoMasterContext> options) : DbContext(options)
{
    public DbSet<Rota> Rotas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rota>().HasData(
        new Rota(1, "GRU", "BRC", 10),
        new Rota(2, "BRC", "SCL", 5),
        new Rota(3, "GRU", "CDG", 75),
        new Rota(4, "GRU", "SCL", 20),
        new Rota(5, "GRU", "ORL", 56),
        new Rota(6, "ORL", "CDG", 5),
        new Rota(7, "SCL", "ORL", 20));
    }
}
