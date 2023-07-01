using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages;
using Apontamento.Core.Tools;
using Apontamento.Identidade.Domain.Entities;
using Apontamento.Identidade.Domain.Interfaces;
using MediatR;

namespace Apontamento.Identidade.Domain.Commands.Autenticacao
{
    internal class AutenticacaoCommandHandler : CommandHandler,
        IRequestHandler<LoginCommand, Usuario>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public AutenticacaoCommandHandler(
            IMediatorHandler mediatorHandler,
            IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default;
            }

            string senhaCriptografada = HashPassword.GenerateSHA512String(request.Senha);

            var usuario = _usuarioRepository.RecuperarPorEmailSenha(request.Email, senhaCriptografada);

            if (usuario == default)
            {
                await NotifyError(request, "E-mail ou senha estão incorretos");
                return default;
            }

            return usuario;
        }
    }
}
