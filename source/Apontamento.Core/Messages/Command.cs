using FluentValidation.Results;
using MediatR;

namespace Apontamento.Core.Messages
{
    public abstract class Command<TCommand> : Message, IRequest<TCommand>
    {
        public Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; protected set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
