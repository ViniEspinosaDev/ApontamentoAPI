using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apontamento.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
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
