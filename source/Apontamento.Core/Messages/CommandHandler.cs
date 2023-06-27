using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;

namespace Apontamento.Core.Messages
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CommandHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        protected async Task NotifyError(Message message, string errorMessage)
        {
            var domainNotification = new DomainNotification(key: message.MessageType, value: errorMessage);

            await _mediatorHandler.PublishNotification(domainNotification);
        }

        protected async void NotifyErrors(Message message, IEnumerable<string> errorMessages)
        {
            foreach (var error in errorMessages)
            {
                await NotifyError(message, error);
            }
        }

        protected async void NotifyValidationErrors<TCommand>(Command<TCommand> command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                var domainNotification = new DomainNotification(key: command.MessageType, error.ErrorMessage);

                await _mediatorHandler.PublishNotification(domainNotification);
            }
        }
    }
}
