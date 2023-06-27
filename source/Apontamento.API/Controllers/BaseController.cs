using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Domain.Enums;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using Apontamento.Identidade.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apontamento.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected readonly ILoggedUser _usuarioLogado;

        protected bool UsuarioAutenticado { get; }
        protected bool AcessoAdministrador { get; }
        protected bool AcessoSuperiorAoDev { get; }
        protected Guid UsuarioLogadoId { get; }

        protected BaseController(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler,
            ILoggedUser usuarioLogado)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _usuarioLogado = usuarioLogado;

            if (_usuarioLogado.IsAuthenticated())
            {
                UsuarioAutenticado = true;
                AcessoAdministrador = _usuarioLogado.UserType == ETipoUsuario.Administrador;
                AcessoSuperiorAoDev = AcessoAdministrador || _usuarioLogado.UserType == ETipoUsuario.Agilista;
                UsuarioLogadoId = _usuarioLogado.UserId;
            }
        }

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotification();
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(notification => notification.Value).ToList();
        }

        protected void NotifyError(string code, string message)
        {
            var domainNotification = new DomainNotification(key: code, value: message);

            _mediatorHandler.PublishNotification(domainNotification);
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = GetErrorMessages()
            });
        }
    }
}
