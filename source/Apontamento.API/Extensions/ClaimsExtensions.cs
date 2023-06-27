using Apontamento.Core.Domain.Enums;
using System.Security.Claims;

namespace Apontamento.API.Extensions
{
    public static class ClaimsExtensions
    {
        public const string
            UserId = "UsuarioId",
            Name = "Nome",
            Email = "Email",
            UserType = "TipoUsuario";

        public static string RecuperarIdUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(UserId);
            return claim?.Value;
        }

        public static string RecuperarNomeUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(Name);
            return claim?.Value;
        }

        public static string RecuperarEmailUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(Email);
            return claim?.Value;
        }

        public static ETipoUsuario RecuperarTipoUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(UserType);

            if (string.IsNullOrEmpty(claim?.Value))
                return ETipoUsuario.Nenhum;

            Enum.TryParse(claim.Value, out ETipoUsuario tipoUsuario);

            return tipoUsuario;
        }
    }
}
