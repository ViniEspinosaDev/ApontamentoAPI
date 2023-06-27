namespace Apontamento.API.Controllers.Identidade.InputModels
{
    public class CadastroUsuarioInputModel
    {
        public string Nome { get; protected set; }
        public string Squad { get; protected set; }
        public int TipoUsuario { get; protected set; }
    }
}
