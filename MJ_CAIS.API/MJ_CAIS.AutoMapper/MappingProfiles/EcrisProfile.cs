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

            CreateMap<EEcrisMessage, EcrisMessageGridDTO>()
                .ForMember(d => d.MsgType, opt => opt.MapFrom(src => src.MsgTypeId));

            CreateMap<EEcrisMsgNationality, EcrisMsgNationalityDTO>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Country.Name));

            CreateMap<EEcrisMsgName, EcrisMsgNameDTO>()
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Familyname));
        }
    }
}
