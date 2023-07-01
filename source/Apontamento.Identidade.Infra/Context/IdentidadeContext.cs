using Apontamento.Core.Data;
using Apontamento.Identidade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apontamento.Identidade.Infra.Context
{
    public class IdentidadeContext : DbContext, IUnitOfWork
    {
        private const string ConnectionString = "Server=localhost,1433;Database=ApontamentoDB;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True; Application Name=Apontamento";

        public IdentidadeContext() { }

        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString, options => options
                .EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(3), errorNumbersToAdd: null)
                .MigrationsHistoryTable("EFMigrations"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentidadeContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
