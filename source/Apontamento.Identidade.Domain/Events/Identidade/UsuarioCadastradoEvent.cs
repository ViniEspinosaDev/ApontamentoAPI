using Apontamento.Core.Messages.CommonMessages.DomainEvents;
using Apontamento.Identidade.Domain.Entities;

namespace Apontamento.Identidade.Domain.Events.Identidade
{
    public class UsuarioCadastradoEvent : DomainEvent
    {
        public UsuarioCadastradoEvent(Usuario usuario, string senhaDescriptografada) : base(usuario.Id)
        {
            Usuario = usuario;
        }

        public Usuario Usuario { get; protected set; }
        public string SenhaDescriptografada { get; protected set; }
    }
}
