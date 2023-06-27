using Apontamento.Core.Domain.Enums;
using Apontamento.Core.DomainObjects;

namespace Apontamento.Identidade.Domain.Entities
{
    public class Usuario : BaseEntity, IAggregateRoot
    {
        public Usuario(string nome, Guid squadId, ETipoUsuario tipo)
        {
            Nome = nome;
            SquadId = squadId;
            Tipo = tipo;
        }

        public string Nome { get; protected set; }
        public Guid SquadId { get; protected set; }
        public ETipoUsuario Tipo { get; protected set; }
    }
}
