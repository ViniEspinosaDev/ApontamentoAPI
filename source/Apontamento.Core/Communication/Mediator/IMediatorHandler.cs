using Apontamento.Core.Messages;
using Apontamento.Core.Messages.CommonMessages.DomainEvents;
using Apontamento.Core.Messages.CommonMessages.Notifications;

namespace Apontamento.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<R> SendCommand<R>(Command<R> command);
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T @event) where T : DomainEvent;
    }
}
