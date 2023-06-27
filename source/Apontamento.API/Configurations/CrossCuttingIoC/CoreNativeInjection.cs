using Apontamento.API.Extensions;
using Apontamento.Core.API.Environment;
using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Domain.Interfaces;
using Apontamento.Core.Domain.Services;
using Apontamento.Core.Messages.CommonMessages.Notifications;
using Apontamento.Identidade.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Apontamento.API.Configurations.CrossCuttingIoC
{
    public static class CoreNativeInjection
    {
        private static IEnvironment _environment;

        public static IServiceCollection ConfigurarDependenciasCore(IServiceCollection services, IEnvironment environment)
        {
            _environment = environment;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMailService>(s => new MailService(environment));

            services.AddScoped<ILoggedUser, LoggedUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            ConfigurarDependenciasJwtToken(services);

            return services;
        }

        private static void ConfigurarDependenciasJwtToken(IServiceCollection services)
        {
            services.AddScoped<IJwtExtensions, JwtExtensions>();

            var key = Encoding.ASCII.GetBytes(_environment.ConfiguracaoAplicacao.Segredo);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _environment.ConfiguracaoAplicacao.ValidoEm,
                    ValidIssuer = _environment.ConfiguracaoAplicacao.Emissor
                };
            });
        }
    }
}
