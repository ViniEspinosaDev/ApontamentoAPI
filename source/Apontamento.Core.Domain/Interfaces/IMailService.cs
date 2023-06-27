using Apontamento.Core.Domain.Entities;
using Apontamento.Core.Domain.Models;

namespace Apontamento.Core.Domain.Interfaces
{
    public interface IMailService
    {
        void OverrideMailConfiguration(MailConfiguration mailConfiguration);
        Task SendMailAsync(Mail destiny);
    }
}
