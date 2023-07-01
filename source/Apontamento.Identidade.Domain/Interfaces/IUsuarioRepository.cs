using Apontamento.Core.Data;
using Apontamento.Identidade.Domain.Entities;

namespace Apontamento.Identidade.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void Adicionar(Usuario usuario);

        Usuario RecuperarPorId(Guid usuarioId);
        Usuario RecuperarPorEmail(string email);
        Usuario RecuperarPorNome(string nome);
        Usuario RecuperarPorEmailOuNome(string email, string nome);
        Usuario RecuperarPorEmailSenha(string email, string senha);
    }
}
