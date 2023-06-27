using Apontamento.Core.Messages;
using Apontamento.Core.Messages.CommonMessages.DomainEvents;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Apontamento.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishDomainEvent<T>(T @event) where T : DomainEvent
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<R> SendCommand<R>(Command<R> command)
        {
            return await _mediator.Send(command);
        }
    }
}
