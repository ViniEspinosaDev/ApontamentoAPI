using Apontamento.Core.Data;
using Apontamento.Identidade.Domain.Entities;
using Apontamento.Identidade.Domain.Interfaces;
using Apontamento.Identidade.Infra.Context;

namespace Apontamento.Identidade.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IdentidadeContext _identidadeContext;

        public UsuarioRepository(IdentidadeContext identidadeContext)
        {
            _identidadeContext = identidadeContext;
        }

        public IUnitOfWork UnitOfWork => _identidadeContext;

        public void Adicionar(Usuario usuario)
        {
            _identidadeContext.Add(usuario);
        }

        public void Dispose()
        {
            _identidadeContext?.Dispose();
        }
    }
}
