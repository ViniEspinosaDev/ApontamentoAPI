using Apontamento.Core.Domain.Enums;
using Apontamento.Core.Messages;
using Apontamento.Identidade.Domain.Entities;
using FluentValidation;

namespace Apontamento.Identidade.Domain.Commands.Identidade
{
    public class CadastrarUsuarioCommand : Command<Usuario>
    {
        public CadastrarUsuarioCommand(string nome, Guid squadId, ETipoUsuario tipoUsuario, string email)
        {
            Nome = nome;
            SquadId = squadId;
            TipoUsuario = tipoUsuario;
            Email = email;
        }

        public string Nome { get; protected set; }
        public Guid SquadId { get; protected set; }
        public ETipoUsuario TipoUsuario { get; protected set; }
        public string Email { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarUsuarioCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class CadastrarUsuarioCommandValidation : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O Nome deve ser informado")
                .OverridePropertyName("ValidacaoNome");

            RuleFor(c => c.Nome.Length)
                .GreaterThanOrEqualTo(3).WithMessage("O Nome deve ter no mínimo 3 caracteres")
                .OverridePropertyName("ValidacaoNome");

            RuleFor(c => c.SquadId)
                .NotEqual(Guid.Empty).WithMessage("O Id do squad não pode ser vazio")
                .OverridePropertyName("ValidacaoSquadId");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("O e-mail não é válido")
                .OverridePropertyName("ValidacaoEmail");
        }
    }
}
