using Apontamento.API.Extensions.ViewModels;
using Apontamento.Core.API.Environment;
using Apontamento.Identidade.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apontamento.API.Extensions
{
    public class JwtExtensions : IJwtExtensions
    {
        private readonly IEnvironment _environment;

        public JwtExtensions(IEnvironment environment)
        {
            _environment = environment;
        }

        public JwtTokenViewModel GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimsExtensions.UserId, usuario.Id.ToString()));
            claims.Add(new Claim(ClaimsExtensions.Name, usuario.Nome));
            claims.Add(new Claim(ClaimsExtensions.Email, usuario.Email));
            claims.Add(new Claim(ClaimsExtensions.UserType, usuario.Tipo.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_environment.ConfiguracaoAplicacao.Segredo);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _environment.ConfiguracaoAplicacao.Emissor,
                Audience = _environment.ConfiguracaoAplicacao.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_environment.ConfiguracaoAplicacao.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new JwtTokenViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_environment.ConfiguracaoAplicacao.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    UsuarioId = usuario.Id.ToString(),
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    TipoUsuario = usuario.Tipo.ToString()
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
