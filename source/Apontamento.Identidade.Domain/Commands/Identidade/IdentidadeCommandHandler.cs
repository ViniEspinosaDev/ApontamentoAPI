using Apontamento.Core.Communication.Mediator;
using Apontamento.Core.Messages;
using Apontamento.Core.Tools;
using Apontamento.Identidade.Domain.Entities;
using Apontamento.Identidade.Domain.Events.Identidade;
using Apontamento.Identidade.Domain.Interfaces;
using MediatR;

namespace Apontamento.Identidade.Domain.Commands.Identidade
{
    public class IdentidadeCommandHandler : CommandHandler,
        IRequestHandler<CadastrarUsuarioCommand, Usuario>,
        IRequestHandler<ResetarSenhaCommand, Usuario>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsuarioRepository _usuarioRepository;

        public IdentidadeCommandHandler(IMediatorHandler mediatorHandler, IUsuarioRepository usuarioRepository) : base(mediatorHandler)
        {
            _usuarioRepository = usuarioRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Usuario> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default;
            }

            var usuarioComEmail = _usuarioRepository.RecuperarPorEmail(request.Email);

            if (usuarioComEmail != default)
            {
                await NotifyError(request, "Já existe um usuário com esse e-mail.");
                return default;
            }

            int numeroAleatorio = new Random().Next(maxValue: 1000);

            string senha = $"!{request.Nome.ToLower()}@Upper{numeroAleatorio}";

            var usuario = new Usuario(
                nome: request.Nome,
                squadId: request.SquadId,
                tipo: request.TipoUsuario,
                senha: HashPassword.GenerateSHA512String(senha),
                email: request.Email);

            _usuarioRepository.Adicionar(usuario);

            bool sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await NotifyError(request, "Não foi possível cadastrar usuário.");
                return default;
            }

            await _mediatorHandler.PublishDomainEvent(new UsuarioCadastradoEvent(usuario, senha));

            return usuario;
        }

        public async Task<Usuario> Handle(ResetarSenhaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return default;
            }

            var usuario = _usuarioRepository.RecuperarPorId(request.UsuarioId);

            if (usuario == default)
            {
                await NotifyError(request, "Usuário não encontrado");
                return default;
            }

            usuario.AlterarSenha(HashPassword.GenerateSHA512String(request.Senha));

            bool sucesso = await _usuarioRepository.UnitOfWork.Commit();

            if (!sucesso)
            {
                await NotifyError(request, "Não foi possível atualizar usuário.");
                return default;
            }

            return usuario;
        }
    }
}
