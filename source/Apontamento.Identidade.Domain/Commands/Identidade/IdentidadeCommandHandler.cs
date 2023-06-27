using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages;
using Apontamento.Core.Tools;
using Apontamento.Identidade.Domain.Entities;
using Apontamento.Identidade.Domain.Events.Identidade;
using Apontamento.Identidade.Domain.Interfaces;
using MediatR;

namespace Apontamento.Identidade.Domain.Commands.Identidade
{
    public class IdentidadeCommandHandler : CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, Usuario>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public IdentidadeCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _usuarioRepository = usuarioRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Usuario> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default;
            }

            string senha = $"{request.Nome.ToLower()}@Upper";

            var usuario = new Usuario(
                nome: request.Nome,
                squadId: request.SquadId,
                tipo: request.TipoUsuario,
                senha: HashPassword.GenerateSHA512String(senha),
                email: request.Email);

            _usuarioRepository.Adicionar(usuario);

            bool sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await NotifyError(request, "Não foi possível cadastrar usuário.");
                return default;
            }

            await _mediatorHandler.PublishDomainEvent(new UsuarioCadastradoEvent(usuario, senha));

            return usuario;
        }
    }
}
