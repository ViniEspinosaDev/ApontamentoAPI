using Apontamento.Core.Data;
using Apontamento.Identidade.Domain.Entities;

namespace Apontamento.Identidade.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void Adicionar(Usuario usuario);
    }
}
