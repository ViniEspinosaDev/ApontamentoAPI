using AutoMapper.Configuration.Annotations;

namespace Apontamento.API.Controllers.Identidade.InputModels
{
    public class ResetarSenhaInputModel
    {
        [Ignore]
        public Guid UsuarioId { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
