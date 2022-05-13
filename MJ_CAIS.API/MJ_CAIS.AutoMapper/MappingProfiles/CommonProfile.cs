using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;

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
            CreateMap<BReqStatus, BaseNomenclatureDTO>()
               .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<APurpose, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<APaymentMethod, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<ASrvcResRcptMeth, BaseNomenclatureDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Code));

            CreateMap<GCountry, CountryDTO>();

            CreateMap<GCountry, LookupDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(src => src.Name));

            CreateMap<LookupDTO, GCountry>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<PPersonIdType, BaseNomenclatureDTO>();
        }
    }
}
