using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class EcrisProfile : Profile
    {
        public EcrisProfile()
        {
            CreateMap<EEcrisMessage, EcrisMessageDTO>();
            CreateMap<EcrisMessageDTO, EEcrisMessage>();
        }
    }
}
