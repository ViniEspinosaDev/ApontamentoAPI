namespace Apontamento.API.Extensions.ViewModels
{
    public class JwtTokenViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class UserTokenViewModel
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string TipoUsuario { get; set; }
    }
}
