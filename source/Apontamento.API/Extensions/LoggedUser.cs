using Apontamento.Core.Domain.Enums;
using Apontamento.Identidade.Domain.Interfaces;

namespace Apontamento.API.Extensions
{
    public class LoggedUser : ILoggedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => IsAuthenticated() ? Guid.Parse(_httpContextAccessor.HttpContext.User.RecuperarIdUsuario()) : Guid.Empty;

        public string Name => IsAuthenticated() ? _httpContextAccessor.HttpContext.User.RecuperarNomeUsuario() : string.Empty;

        public string Email => IsAuthenticated() ? _httpContextAccessor.HttpContext.User.RecuperarEmailUsuario() : string.Empty;

        public ETipoUsuario UserType => IsAuthenticated() ? _httpContextAccessor.HttpContext.User.RecuperarTipoUsuario() : ETipoUsuario.Nenhum;

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
