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

        }
    }
}
