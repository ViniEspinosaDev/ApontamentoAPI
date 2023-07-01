namespace Apontamento.API.Controllers.Identidade.InputModels
{
    public class CadastroUsuarioInputModel
    {
        public string Nome { get; set; }
        public Guid SquadId { get; set; }
        public int TipoUsuario { get; set; }
        public string Email { get; set; }
    }
}
