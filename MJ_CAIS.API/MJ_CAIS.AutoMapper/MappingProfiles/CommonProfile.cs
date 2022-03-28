using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<IBaseNomenclature, BaseNomenclatureDTO>();

            CreateMap<BBulletinStatus, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<FbbcDocType, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<FbbcSanctType, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));
        }
    }
}
