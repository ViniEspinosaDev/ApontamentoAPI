using Apontamento.Core.Domain.Models;
using Apontamento.Core.Messages.AppConfiguration;
using Apontamento.Core.Messages.ConnectionStrings;
using Microsoft.Extensions.Options;

namespace Apontamento.Core.API.Environment
{
    public class EnvironmentVariables : IEnvironment
    {
        private readonly MailConfiguration _mailConfiguration;
        private readonly ConnectionStringsConfiguration _connectionStringsConfiguration;
        private readonly AppConfiguration _appConfiguration;

        public EnvironmentVariables(
            IOptions<MailConfiguration> mailConfiguration,
            IOptions<ConnectionStringsConfiguration> connectionStringsConfiguration,
            IOptions<AppConfiguration> appConfiguration)
        {
            _mailConfiguration = mailConfiguration.Value;
            _connectionStringsConfiguration = connectionStringsConfiguration.Value;
            _appConfiguration = appConfiguration.Value;
        }
        private string RecuperarVariavelAmbiente(string rotulo) => System.Environment.GetEnvironmentVariable(rotulo);

        private bool Desenvolvimento => RecuperarVariavelAmbiente(EnvironmentConstants.Ambiente) == EnvironmentConstants.Desenvolvimento;
        private bool Producao => RecuperarVariavelAmbiente(EnvironmentConstants.Ambiente) == EnvironmentConstants.Producao;
        private string SQL_Servidor => RecuperarVariavelAmbiente(EnvironmentConstants.SQL_Servidor);
        private string SQL_NomeBD => RecuperarVariavelAmbiente(EnvironmentConstants.SQL_NomeBD);
        private string SQL_Usuario => RecuperarVariavelAmbiente(EnvironmentConstants.SQL_Usuario);
        private string SQL_Senha => RecuperarVariavelAmbiente(EnvironmentConstants.SQL_Senha);
        private string SQL_NomeAPP => RecuperarVariavelAmbiente(EnvironmentConstants.SQL_NomeAPP);
        private string MAIL_SMTP => RecuperarVariavelAmbiente(EnvironmentConstants.MAIL_SMTP);
        private string MAIL_Porta => RecuperarVariavelAmbiente(EnvironmentConstants.MAIL_Porta);
        private string MAIL_Endereco => RecuperarVariavelAmbiente(EnvironmentConstants.MAIL_Endereco);
        private string MAIL_Senha => RecuperarVariavelAmbiente(EnvironmentConstants.MAIL_Senha);
        private string MAIL_UsarSSL => RecuperarVariavelAmbiente(EnvironmentConstants.MAIL_UsarSSL);

        public string ConexaoSQL
        {
            get
            {
                if (Desenvolvimento)
                    return _connectionStringsConfiguration.SqlConnection;

                return $"Server={SQL_Servidor}; Initial Catalog={SQL_NomeBD}; User={SQL_Usuario}; Password={SQL_Senha}; MultipleActiveResultSets=True; Application Name={SQL_NomeAPP}";
            }
        }

        public MailConfiguration ConfiguracaoEmail
        {
            get
            {
                if (Desenvolvimento)
                    return _mailConfiguration;

                return new MailConfiguration()
                {
                    SMTP = MAIL_SMTP,
                    Port = int.Parse(MAIL_Porta),
                    Address = MAIL_Endereco,
                    Password = MAIL_Senha,
                    SandBox = true,
                    UseSsl = bool.Parse(MAIL_UsarSSL)
                };
            }
        }

        public AppConfiguration ConfiguracaoAplicacao
        {
            get
            {
                return _appConfiguration;
            }
        }
    }
}
