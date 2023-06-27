using Apontamento.Core.API.Environment;
using Apontamento.Identidade.Domain.Commands.Identidade;
using Apontamento.Identidade.Domain.Entities;
using Apontamento.Identidade.Domain.Events.Identidade;
using Apontamento.Identidade.Domain.Interfaces;
using Apontamento.Identidade.Infra.Context;
using Apontamento.Identidade.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Apontamento.Identidade.CrossCuttingIoC
{
    public static class IdentidadeNativeInjection
    {
        private static IEnvironment _environment;

        public static IServiceCollection ConfigurarDependenciasIdentidade(IServiceCollection services, IEnvironment environment)
        {
            _environment = environment;

            ConfigurarDependenciasBancoDados(services);
            ConfigurarDependenciasCommand(services);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasEvent(services);

            return services;
        }

        private static void ConfigurarDependenciasBancoDados(IServiceCollection services)
        {
            services.AddDbContext<IdentidadeContext>(options => options.UseSqlServer(_environment.ConexaoSQL));
        }

        private static void ConfigurarDependenciasEvent(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<UsuarioCadastradoEvent>, IdentidadeEventHandler>();
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarUsuarioCommand, Usuario>, IdentidadeCommandHandler>();
        }
    }
}
