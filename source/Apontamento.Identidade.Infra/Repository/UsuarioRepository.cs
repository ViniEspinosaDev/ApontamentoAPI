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
            _identidadeContext.Usuario.Add(usuario);
        }

        public Usuario RecuperarPorEmail(string email)
        {
            return _identidadeContext.Usuario.FirstOrDefault(usuario => usuario.Email == email);
        }

        public Usuario RecuperarPorEmailOuNome(string email, string nome)
        {
            return _identidadeContext.Usuario.FirstOrDefault(usuario => usuario.Email == email || usuario.Nome == nome);
        }

        public Usuario RecuperarPorNome(string nome)
        {
            return _identidadeContext.Usuario.FirstOrDefault(usuario => usuario.Nome == nome);
        }

        public Usuario RecuperarPorEmailSenha(string email, string senha)
        {
            return _identidadeContext.Usuario.FirstOrDefault(usuario => usuario.Email == email && usuario.Senha == senha);
        }
        public Usuario RecuperarPorId(Guid usuarioId)
        {
            return _identidadeContext.Usuario.Find(usuarioId);
        }

        public void Dispose()
        {
            _identidadeContext?.Dispose();
        }
    }
}
