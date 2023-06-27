namespace Apontamento.Core.API.Environment
{
    public static class EnvironmentConstants
    {
        public const string
            Ambiente = "ASPNETCORE_ENVIRONMENT",
            Desenvolvimento = "Dev",
            Producao = "Prod";

        public const string
            SQL_Servidor = "SQL_SERVER",
            SQL_NomeBD = "SQL_NAME_DB",
            SQL_Usuario = "SQL_USER",
            SQL_Senha = "SQL_PASSWORD",
            SQL_NomeAPP = "SQL_APP_NAME";

        public const string
            MAIL_SMTP = "MAIL_SMTP",
            MAIL_Porta = "MAIL_PORT",
            MAIL_Endereco = "MAIL_ADDRESS",
            MAIL_Senha = "MAIL_PASSWORD",
            MAIL_UsarSSL = "MAIL_USE_SSL";
    }
}
