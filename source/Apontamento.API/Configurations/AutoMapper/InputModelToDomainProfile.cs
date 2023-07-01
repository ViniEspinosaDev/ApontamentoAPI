using Apontamento.API.Controllers.Identidade.InputModels;
using Apontamento.Identidade.Domain.Commands.Identidade;
using AutoMapper;

namespace Apontamento.API.Configurations.AutoMapper
{
    public class InputModelToDomainProfile : Profile
    {
        public InputModelToDomainProfile()
        {
            MapeiaContextoIdentidade();
        }

        private void MapeiaContextoIdentidade()
        {
            CreateMap<CadastroUsuarioInputModel, CadastrarUsuarioCommand>()
                .ForMember(c => c.Nome, opt => opt.MapFrom(m => m.Nome))
                .ForMember(c => c.SquadId, opt => opt.MapFrom(m => m.SquadId))
                .ForMember(c => c.TipoUsuario, opt => opt.MapFrom(m => m.TipoUsuario))
                .ForMember(c => c.Email, opt => opt.MapFrom(m => m.Email));
        }
    }
}
