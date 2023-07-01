using Apontamento.Core.Messages;
using Apontamento.Identidade.Domain.Entities;
using FluentValidation;

namespace Apontamento.Identidade.Domain.Commands.Autenticacao
{
    public class LoginCommand : Command<Usuario>
    {
        public LoginCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; protected set; }
        public string Senha { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new LoginCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }

    public class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("O e-mail não é válido")
                .OverridePropertyName("ValidacaoEmail");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("A senha é obriatória")
                .OverridePropertyName("ValidacaoSenha");
        }
    }
}
