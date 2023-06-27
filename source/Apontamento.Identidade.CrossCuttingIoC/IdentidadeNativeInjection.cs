using Apontamento.Identidade.Domain.Commands.Identidade;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Apontamento.Identidade.CrossCuttingIoC
{
    public static class IdentidadeNativeInjection
    {
        public static IServiceCollection ConfigurarDependenciasIdentidade(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarUsuarioCommand, bool>, IdentidadeCommandHandler>();

            return services;
        }
    }
}
