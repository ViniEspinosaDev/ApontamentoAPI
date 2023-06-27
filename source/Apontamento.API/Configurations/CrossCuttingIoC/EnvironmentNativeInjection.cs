using Apontamento.Core.API.Environment;
using Apontamento.Core.Domain.Models;
using Apontamento.Core.Messages.AppConfiguration;
using Apontamento.Core.Messages.ConnectionStrings;

namespace Apontamento.API.Configurations.CrossCuttingIoC
{
    public static class EnvironmentNativeInjector
    {
        public static void ConfigurarVariaveisAmbiente(IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"))
                .Configure<ConnectionStringsConfiguration>(configuration.GetSection("ConnectionStrings"))
                .Configure<AppConfiguration>(configuration.GetSection("AppSettings"))
                .AddScoped<IEnvironment, EnvironmentVariables>();
        }
    }
}
