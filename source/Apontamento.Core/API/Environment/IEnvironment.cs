using Apontamento.Core.Domain.Models;
using Apontamento.Core.Messages.AppConfiguration;

namespace Apontamento.Core.API.Environment
{
    public interface IEnvironment
    {
        string ConexaoSQL { get; }
        MailConfiguration ConfiguracaoEmail { get; }
        AppConfiguration ConfiguracaoAplicacao { get; }
    }
}
