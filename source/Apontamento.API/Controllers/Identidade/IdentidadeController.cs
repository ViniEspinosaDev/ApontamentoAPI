using Apontamento.Api.Controllers;
using Apontamento.API.Controllers.Identidade.InputModels;
using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using Apontamento.Identidade.Domain.Commands.Identidade;
using Apontamento.Identidade.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apontamento.API.Controllers.Identidade
{
    [Authorize]
    public class IdentidadeController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMapper _mapper;

        public IdentidadeController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            ILoggedUser usuarioLogado,
            IMapper mapper) : base(notifications, mediatorHandler, usuarioLogado)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
        }

        // TODO: Remover o AllowAnounymous
        [AllowAnonymous]
        [HttpPost("cadastrar-usuario")]
        public async Task<ActionResult> CadastrarUsuario(CadastroUsuarioInputModel cadastroUsuarioInputModel)
        {
            //if (!AcessoSuperiorAoDev)
            //{
            //    NotifyError("-1", "É necessário um nível de acesso superior");
            //    return CustomResponse();
            //}

            var comando = _mapper.Map<CadastrarUsuarioCommand>(cadastroUsuarioInputModel);

            var usuario = await _mediatorHandler.SendCommand(comando);

            string mensagem = $"Usuário criado com sucesso";

            return CustomResponse(mensagem);
        }

        // TODO: Remover o AllowAnounymous
        [AllowAnonymous]
        [HttpPut("resetar-senha")]
        public async Task<ActionResult> ResetarSenha(ResetarSenhaInputModel resetarSenhaInputModel)
        {
            // Resetar flag na tabela de Usuário (PrimeiroLogin = true | Senha = Padrão)

            return CustomResponse();
        }
    }
}
