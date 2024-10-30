using Microsoft.EntityFrameworkCore;
using SistemaBoleto.Models;

namespace SistemaBoleto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Boleto> Boletos { get; set; } = default!;
        public DbSet<Banco> Bancos { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boleto>()
                .HasOne(b => b.Banco)
                .WithMany(b => b.Boletos)
                .HasForeignKey(b => b.BancoId);
        }

    }
}
