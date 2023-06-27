using Apontamento.Core.Domain.Enums;
using Apontamento.Core.DomainObjects;

namespace Apontamento.Identidade.Domain.Entities
{
    public class Usuario : BaseEntity, IAggregateRoot
    {
        public Usuario(string nome, Guid squadId, ETipoUsuario tipo, string senha, string email)
        {
            Nome = nome;
            SquadId = squadId;
            Tipo = tipo;
            Senha = senha;
            PrimeiroLogin = true;
            DataCadastro = DateTime.Now;
            Desativado = false;
            Email = email;
        }

        public string Nome { get; protected set; }
        public Guid SquadId { get; protected set; }
        public ETipoUsuario Tipo { get; protected set; }
        public string Senha { get; protected set; }
        public string Email { get; protected set; }
        public bool PrimeiroLogin { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
        public bool Desativado { get; protected set; }
    }
}
