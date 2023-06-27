using Apontamento.Core.Data;
using Apontamento.Identidade.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apontamento.Identidade.Infra.Context
{
    public class IdentidadeContext : DbContext, IUnitOfWork
    {
        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

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
