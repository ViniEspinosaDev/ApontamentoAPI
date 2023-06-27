using MediatR;

namespace Apontamento.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; }
    }
}
