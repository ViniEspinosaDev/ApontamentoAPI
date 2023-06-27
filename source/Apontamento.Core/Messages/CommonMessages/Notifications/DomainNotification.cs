using MediatR;

namespace Apontamento.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            Version = 1;
            Key = key;
            Value = value;
        }

        public Guid DomainNotificationId { get; protected set; }
        public DateTime Timestamp { get; protected set; }
        public string Key { get; protected set; }
        public string Value { get; protected set; }
        public int Version { get; protected set; }
    }
}
