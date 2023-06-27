using MediatR;

namespace Apontamento.Identidade.Domain.Events.Identidade
{
    public class IdentidadeEventHandler : INotificationHandler<UsuarioCadastradoEvent>
    {


        public async Task Handle(UsuarioCadastradoEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Enviar e-mail com a senha Login: Email | Senha: Senha

            await Task.CompletedTask;
        }
    }
}
