using Apontamento.Core.Domain.Enums;

namespace Apontamento.Identidade.Domain.Interfaces
{
    public interface ILoggedUser
    {
        bool IsAuthenticated();
        Guid UserId { get; }
        string Name { get; }
        string Email { get; }
        ETipoUsuario UserType { get; }
    }
}
