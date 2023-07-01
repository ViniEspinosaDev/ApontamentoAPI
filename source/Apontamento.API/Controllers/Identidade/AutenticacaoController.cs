using Apontamento.Api.Controllers;
using Apontamento.API.Controllers.Identidade.InputModels;
using Apontamento.API.Extensions;
using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using Apontamento.Identidade.Domain.Commands.Autenticacao;
using Apontamento.Identidade.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apontamento.API.Controllers.Identidade
{
    public class AutenticacaoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IJwtExtensions _jwtExtensions;

        public AutenticacaoController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            ILoggedUser usuarioLogado,
            IMapper mapper,
            IJwtExtensions jwtExtensions) : base(notifications, mediatorHandler, usuarioLogado)
        {
            _mediatorHandler = mediatorHandler;
            _mapper = mapper;
            _jwtExtensions = jwtExtensions;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginInputModel loginInputModel)
        {
            var comando = _mapper.Map<LoginCommand>(loginInputModel);

            var usuario = await _mediatorHandler.SendCommand(comando);

            if (usuario is null) return CustomResponse();

            return CustomResponse(_jwtExtensions.GenerateJwtToken(usuario));
        }
    }
}
