using Apontamento.API.Extensions.ViewModels;
using Apontamento.Identidade.Domain.Entities;

namespace Apontamento.API.Extensions
{
    public interface IJwtExtensions
    {
        JwtTokenViewModel GenerateJwtToken(Usuario usuario);
    }
}
