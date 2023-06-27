using Apontamento.Api.Controllers;
using Apontamento.API.Controllers.Identidade.InputModels;
using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apontamento.API.Controllers.Identidade
{
    [Authorize]
    public class IdentidadeController : BaseController
    {
        public IdentidadeController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult> CadastrarUsuario(CadastroUsuarioInputModel cadastroUsuarioInputModel)
        {
            return CustomResponse();
        }

        [HttpPut("resetar-senha")]
        public async Task<ActionResult> ResetarSenha(ResetarSenhaInputModel resetarSenhaInputModel)
        {
            // Resetar flag na tabela de Usuário (PrimeiroLogin = true | Senha = Padrão)

            return CustomResponse();
        }
    }
}
