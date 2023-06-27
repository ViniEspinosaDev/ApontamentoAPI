using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace Apontamento.API.Configurations.CrossCuttingIoC
{
    public static class CoreNativeInjection
    {
        public static IServiceCollection ConfigurarDependenciasCore(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            return services;
        }
    }
}
