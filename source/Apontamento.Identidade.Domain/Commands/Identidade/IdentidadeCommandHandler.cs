using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages;
using Apontamento.Identidade.Domain.Entities;
using MediatR;

namespace Apontamento.Identidade.Domain.Commands.Identidade
{
    public class IdentidadeCommandHandler : CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, bool>
    {
        public IdentidadeCommandHandler(IMediatorHandler mediatorHandler) : base(mediatorHandler)
        {
        }

        public Task<bool> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var usuario = new Usuario(
                nome: request.Nome,
                squadId: request.SquadId,
                tipo: request.TipoUsuario);

            return Task.FromResult(true);
        }
    }
}
