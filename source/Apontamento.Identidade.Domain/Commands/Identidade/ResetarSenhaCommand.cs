using Apontamento.Core.Messages;
using Apontamento.Identidade.Domain.Entities;
using FluentValidation;

namespace Apontamento.Identidade.Domain.Commands.Identidade
{
    public class ResetarSenhaCommand : Command<Usuario>
    {
        public ResetarSenhaCommand(Guid usuarioId, string senha, string confirmacaoSenha)
        {
            UsuarioId = usuarioId;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }

        public Guid UsuarioId { get; protected set; }
        public string Senha { get; protected set; }
        public string ConfirmacaoSenha { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new ResetarSenhaCommandValidation().Validate(this);

                return ValidationResult.IsValid;
        }
    }

    public class ResetarSenhaCommandValidation : AbstractValidator<ResetarSenhaCommand>
    {
        public ResetarSenhaCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não pode ser vazio")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c =>  c.Senha)
                .NotEmpty().WithMessage("Senha não pode ser vazia")
                .OverridePropertyName("ValidacaoSenhaVazia");

            RuleFor(c => c.Senha.Length)
                .GreaterThanOrEqualTo(6).WithMessage("A senha deve ter no mínimo 6 caracteres")
                .OverridePropertyName("ValidacaoSenhaCaracteres");

            RuleFor(c => c.Senha != c.ConfirmacaoSenha)
                .NotEqual(false).WithMessage("As senhas não são iguais")
                .OverridePropertyName("ValidacaoSenhaDiferentes");
        }
    }
}
