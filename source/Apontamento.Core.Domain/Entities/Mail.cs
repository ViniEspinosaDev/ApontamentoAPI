using Apontamento.Core.DomainObjects;
using System.Net.Mail;

namespace Apontamento.Core.Domain.Entities
{
    public class Mail : BaseEntity
    {
        public Mail(string mailAddress, string bodyText, string subject)
        {
            MailAddress = mailAddress;
            BodyText = bodyText;
            Subject = subject;
        }
        public string MailAddress { get; protected set; }
        public string BodyText { get; protected set; }
        public string Subject { get; protected set; }
        public List<Attachment> Attachments { get; protected set; }

        public void AddAttachment(Attachment attachment)
        {
            if (Attachments == null) Attachments = new List<Attachment>();

            Attachments.Add(attachment);
        }
    }
}
