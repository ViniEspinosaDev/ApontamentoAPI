namespace Apontamento.Core.Domain.Models
{
    public class MailConfiguration
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool SandBox { get; set; }
        public bool UseSsl { get; set; }

        public override string ToString()
        {
            return $"SMTP: {SMTP} | Port: {Port} | Address: {Address} | Password: {Password} | SandBox: {SandBox} | UseSsl: {UseSsl}";
        }
    }
}
